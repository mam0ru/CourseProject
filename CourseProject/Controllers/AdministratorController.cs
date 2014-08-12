using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;

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
            return View( UserManager.Users);
        }

        [HttpGet]
        public ActionResult MakeAdministrator(int Id)
        {
            //TODO
            //Request.Form.Get()
            return RedirectToAction("AdministratorMain");
        }

        [HttpGet]
        public ActionResult BlockUser(int Id)
        {
            //TODO
            return RedirectToAction("AdministratorMain");
        }

        [HttpGet]
        public ActionResult DropUserPassword(int Id)
        {
            //TODO
            return RedirectToAction("AdministratorMain");
        }

        [HttpGet]
        public ActionResult DeleteUser(int Id)
        {
            //TODO
            return RedirectToAction("AdministratorMain");
        }
    }
}