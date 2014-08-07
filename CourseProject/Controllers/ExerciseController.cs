using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using CloudinaryDotNet.Actions;
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

        public ActionResult AddAnswer(int id)
        {
            //MvcApplication.dataBase.ExerciseRepository.dbSet.Single(exersise => exersise.Id == id).Active = !isActive;
            // context.Exercises.
            return View(MvcApplication.dataBase.ExerciseRepository.dbSet);
        }

        [HttpPost]
        public ActionResult UploadImage()
        {
            Cloudinary cloudinary = new Cloudinary(account);
            //var param = new ImageUploadParams()
            //{
            //    File = new FileDescription(fileUpload.FileName, fileUpload.InputStream)
            //};

            //var uploadResult = cloudinary.Upload(param);

            //var uplPath = uploadResult.Uri;
            return RedirectToAction("Index","Home");
        }

    }
}