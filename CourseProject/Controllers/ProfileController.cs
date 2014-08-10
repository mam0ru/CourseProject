using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseProject.Models;
using CourseProject.Repository;
using CourseProject.Repository.Interfaces;
using Microsoft.AspNet.Identity;

namespace CourseProject.Controllers
{
    // [Authorize(Roles = "user")]
    [Authorize]
    public class ProfileController : Controller
     {
         private readonly IApplicationUserRepository applicationUserRepository;

         private readonly IExerciseRepository exerciseRepository;

         public ProfileController(IApplicationUserRepository applicationUserRepository, IExerciseRepository exerciseRepository)
         {
             this.applicationUserRepository = applicationUserRepository;
             this.exerciseRepository = exerciseRepository;
         }

        [HttpGet]
        public ActionResult MyProfile()
        {
            return View(applicationUserRepository.GetByID(User.Identity.GetUserId()));
        }

        [HttpPost]
        public ActionResult MakeActiveUnactive(int id, bool isActive)
        {
            var exercise = exerciseRepository.GetByID(id);
            exercise.Active = !isActive;
            exerciseRepository.Update(exercise);
            return View("MyProfile", applicationUserRepository.GetByID(User.Identity.GetUserId()));
        }

        [HttpGet]
        public ActionResult ShowProfile(int id)
        {
            return View(applicationUserRepository.GetByID(id));
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