using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
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

        private readonly IExerciseRepository exerciseRepository;

        public ProfileController(ApplicationUserManager userManager, IExerciseRepository exerciseRepository)
        {
            this.userManager = userManager;
            this.exerciseRepository = exerciseRepository;
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
            ViewBag.Rating = UserManager.Users.OrderByDescending(user => user.RightAnswers.Count()).ToList().FindIndex(user=>user.Id==currentUser.Id)+1;
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
        public ActionResult ShowProfile(int id)
        {
            return View(UserManager.FindById(User.Identity.GetUserId()));
        }

        public ActionResult Rating()
        {
            var users = UserManager.Users.OrderByDescending(user => user.RightAnswers.Count());
            return View("Rating", users);//applicationUserRepository.Get().OrderBy(user => user.RightAnswers.Count()));
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