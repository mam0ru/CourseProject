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
    }
}