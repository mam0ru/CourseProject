using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseProject.View_Models;

namespace CourseProject.Controllers
{
    [Authorize]
    public class ExerciseController : Controller
    {
        // GET: Exrcise
        [HttpGet]
        public ActionResult CreateExercise()
        {
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

        public ActionResult AddAnswer(int id)
        {
            //MvcApplication.dataBase.ExerciseRepository.dbSet.Single(exersise => exersise.Id == id).Active = !isActive;
            // context.Exercises.
            return View(MvcApplication.dataBase.ExerciseRepository.dbSet);
        }

    }
}