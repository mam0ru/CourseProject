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
    [Authorize]
    public class ProfileController : Controller
    {
        private ApplicationUserManager userManager;
        private readonly IApplicationUserRepository applicationUserRepository;

        private readonly IExerciseRepository exerciseRepository;

        public ProfileController(ApplicationUserManager userManager, IExerciseRepository exerciseRepository)
        {
            this.userManager = userManager;
            this.applicationUserRepository = applicationUserRepository;
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

        [HttpGet]
        public ActionResult MyProfile()
        {
            var currentUser = UserManager.Users.First(user => user.Id == User.Identity.GetUserId());
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