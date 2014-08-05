using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseProject.Controllers
{
    public class ExerciseController : Controller
    {
        // GET: Exrcise
        public ActionResult CreateExercise()
        {
            return View();
        }
    }
}