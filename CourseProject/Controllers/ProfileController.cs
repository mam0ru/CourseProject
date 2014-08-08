using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseProject.Models;
using Microsoft.AspNet.Identity;

namespace CourseProject.Controllers
{
     [Authorize(Roles = "user")]
    public class ProfileController : Controller
    {
        // GET: Profile
        [HttpGet]
        public ActionResult MyProfile()
        {
            return View(MvcApplication.dataBase.UserRepository.GetByID(User.Identity.GetUserId()));
        }

        [HttpPost]
        public ActionResult MakeActiveUnactive(int id, bool isActive)
        {
            var exercise = MvcApplication.dataBase.ExerciseRepository.dbSet.Single(exersise => exersise.Id == id);
            exercise.Active = !isActive;
            MvcApplication.dataBase.ExerciseRepository.Update(exercise);
            return View("MyProfile", MvcApplication.dataBase.UserRepository.GetByID(User.Identity.GetUserId()));
        }

        [HttpGet]
        public ActionResult ShowProfile(int id)
        {
            return View(MvcApplication.dataBase.UserRepository.GetByID(id));
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