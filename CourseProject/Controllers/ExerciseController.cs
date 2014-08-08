using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using CloudinaryDotNet.Actions;
using CourseProject.Models;
using CourseProject.View_Models;
using CloudinaryDotNet;

namespace CourseProject.Controllers
{
    [Authorize]
    public class ExerciseController : Controller
    {
        private Account account = new Account("dkfntkp0r", "284111675587747", "shagM6LcW1MFmkWU60j2L9FWPps");
        // GET: Exrcise
        [HttpGet]
        public ActionResult CreateExercise()
        {
            ViewBag.categories = MvcApplication.dataBase.CategoryRepository.Get().Select(category => category.Text);
            return View();
        }

        [HttpPost]
        public ActionResult CreateExercise(ExerciseCreateViewModel model)
        {
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public ActionResult ShowExercise()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ShowExercise(int id)
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ShowExercisesWithTag(string tag)
        {
            return RedirectToAction("Index", "Home"); //"ShowExercise", "Exersise",);
        }

        [HttpGet]
        public ActionResult ShowExercisesWithTag()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddAnswer()
        {
            //MvcApplication.dataBase.ExerciseRepository.dbSet.Single(exersise => exersise.Id == id).Active = !isActive;
            // context.Exercises.
            return RedirectToAction("MyProfile", "Profile");
        }

        [HttpPost]
        public ActionResult AddAnswer(int id)
        {
            //var exercise = MvcApplication.dataBase.ExerciseRepository.dbSet.Single(exersise => exersise.Id == id);
           // exercise.Answers.Add();
            // context.Exercises.

            return View(MvcApplication.dataBase.ExerciseRepository.dbSet);
        }

        [HttpPost]
        public ActionResult UploadImage()
        {
            Cloudinary cloudinary = new Cloudinary(account);
            HttpPostedFileBase image = Request.Files["imageupload"];
            var param = new ImageUploadParams()
            {
                File = new FileDescription(image.FileName, image.InputStream)
            };

            var uploadResult = cloudinary.Upload(param);
            var uplPath = uploadResult.Uri.AbsoluteUri;
            Picture uploadedPicture = new Picture();
            uploadedPicture.Path = uplPath;
            uploadedPicture.Name = uploadResult.PublicId;
            MvcApplication.dataBase.PictureRepository.Insert(uploadedPicture);
            MvcApplication.dataBase.Save();
            return Json(new {path = uplPath, });
        }

    }
}