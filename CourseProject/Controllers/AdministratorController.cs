using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using CourseProject.View_Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using CourseProject.View_Models;

namespace CourseProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdministratorController : Controller
    {
        private ApplicationUserManager userManager;

          public AdministratorController(ApplicationUserManager userManager)
        {
            this.userManager = userManager;
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
        public ActionResult AdministratorMain()
        {
            List<UserForAdministratorMainViewModel> users = new List<UserForAdministratorMainViewModel>();
            foreach (var user in userManager.Users)
            {
                UserForAdministratorMainViewModel userForAdmin = new UserForAdministratorMainViewModel();
                userForAdmin.Admin = userManager.IsInRole(user.Id, "admin");
                userForAdmin.Blocked = false;
                userForAdmin.Deleted = false;
                userForAdmin.DroppedPassword = false;
                userForAdmin.Email = user.Email;
                userForAdmin.Name = user.UserName;
                userForAdmin.SolvedExercises = user.RightAnswers;
                userForAdmin.UsersExercises = user.Exercises;
                userForAdmin.Id = user.Id;
                users.Add(userForAdmin);
            }
            return View(users);
        }

        [HttpPost]
        public ActionResult AdministratorMain(List<UserForAdministratorMainViewModel> users)
        {
            foreach (var user in userManager.Users)
            {
                UserForAdministratorMainViewModel userForAdmin = new UserForAdministratorMainViewModel();
                userForAdmin.Admin = userManager.IsInRole(user.Id, "admin");
                userForAdmin.Blocked = false;
                userForAdmin.Deleted = false;
                userForAdmin.DroppedPassword = false;
                userForAdmin.Email = user.Email;
                userForAdmin.Name = user.UserName;
                userForAdmin.SolvedExercises = user.RightAnswers;
                userForAdmin.UsersExercises = user.Exercises;
                userForAdmin.Id = user.Id;
                users.Add(userForAdmin);
            }
            return View(users);
        }

       
    }
}