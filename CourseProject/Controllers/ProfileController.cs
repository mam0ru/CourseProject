using System;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Web;
using System.Web.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CourseProject.Models;
using CourseProject.Repository;
using CourseProject.Repository.Interfaces;
using Lucene.Net.Documents;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MultilingualSite.Filters;

namespace CourseProject.Controllers
{
     [Culture]
    public class ProfileController : Controller
    {
        private ApplicationUserManager userManager;

        private readonly IPictureRepository pictureRepository;

        private Account account = new Account("dkfntkp0r", "284111675587747", "shagM6LcW1MFmkWU60j2L9FWPps");

        private readonly IExerciseRepository exerciseRepository;

        public ProfileController(IExerciseRepository exerciseRepository, IPictureRepository pictureRepository)
        {
            this.exerciseRepository = exerciseRepository;
            this.pictureRepository = pictureRepository;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                userManager = value;
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult MyProfile()
        {
            var currentUser = UserManager.FindById(User.Identity.GetUserId());
            return View(currentUser);//applicationUserRepository.GetByID(User.Identity.GetUserId()));
        }

        [Authorize]
        [HttpGet]
        public ActionResult MakeActiveUnactive(int id, bool isActive)
        {
            var exercise = exerciseRepository.GetByID(id);
            exercise.Active = !isActive;
            exerciseRepository.Update(exercise);
            string returnUrl = Request.UrlReferrer.AbsolutePath;
            return Redirect(returnUrl);
        }

        [HttpGet]
        public ActionResult ShowProfile(string id)
        {
            var user = UserManager.FindById(id);
            return View(user);
        }

        public ActionResult Rating()
        {
            var users = UserManager.Users.OrderByDescending(user => user.Rating);
            var list = users.ToList();
            return View(users);
        }

        public ActionResult HighRatingUsers()
        {
            var users = UserManager.Users.OrderByDescending(user => user.Rating);
            return PartialView("_HighRatingUsersPartial", users.ToList());
        }

        public ActionResult SetAvatar()
        {
            Cloudinary cloudinary = new Cloudinary(account);
            HttpPostedFileBase image = Request.Files["imageupload"];
            ImageUploadParams param = new ImageUploadParams()
            {
                File = new FileDescription(image.FileName, image.InputStream)
            };

            ImageUploadResult uploadResult = cloudinary.Upload(param);
            string uplPath;
            uplPath = uploadResult.Uri.AbsoluteUri;
            string label = "upload/";
            int insertIndex = uplPath.IndexOf(label) + label.Length;
            string setImageSize = "c_scale,w_100/";
            uplPath = uplPath.Insert(insertIndex, setImageSize);
            var user = UserManager.FindById(User.Identity.GetUserId());
            user.ImagePath = uplPath;
            UserManager.Update(user);

            return Json(new { path = uplPath });
        }
    }
}