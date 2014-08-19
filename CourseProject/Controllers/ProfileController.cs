using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CourseProject.Models;
using CourseProject.Repository;
using CourseProject.Repository.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace CourseProject.Controllers
{
    // [Authorize(Roles = "user")]

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
            ViewBag.Rating = UserManager.Users.OrderByDescending(user => user.RightAnswers.Count()).ToList().FindIndex(user => user.Id == currentUser.Id) + 1;
            return View(currentUser);//applicationUserRepository.GetByID(User.Identity.GetUserId()));
        }

        [Authorize]
        [HttpPost]
        public ActionResult MakeActiveUnactive(int id, bool isActive)
        {
            var exercise = exerciseRepository.GetByID(id);
            exercise.Active = !isActive;
            exerciseRepository.Update(exercise);
            //UserManager.FindById(User.Identity.GetUserId()).Exercises.Remove()
            return View("MyProfile", UserManager.FindById(User.Identity.GetUserId()));
        }

        [HttpGet]
        public ActionResult ShowProfile(string id)
        {
            var user = UserManager.FindById(id);
            return View(user);
        }

        public ActionResult Rating()
        {
            var users = UserManager.Users.OrderByDescending(user => user.RightAnswers.Count());
            return View(users);
        }

        public ActionResult HighRatingUsers()
        {
            var users = UserManager.Users.OrderByDescending(user => user.RightAnswers.Count());
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

            Picture uploadedPicture = new Picture();
            uploadedPicture.Path = uplPath;
            uploadedPicture.Name = uploadResult.PublicId;
            pictureRepository.Insert(uploadedPicture);

            var user = UserManager.FindById(User.Identity.GetUserId());
            user.ImagePath = uplPath;
            UserManager.Update(user);

            return Json(new { path = uplPath });
        }
        /*
       [HttpPost]
       public ActionResult ShowProfile(int id)
       {
           return View(MvcApplication.dataBase.UserRepository.GetByID(id));
       }*/
        /*[HttpPost]
        public ActionResult MyProfile()
        {
            return RedirectToActionPermanent(context.Exercises);
        }*/
    }
}