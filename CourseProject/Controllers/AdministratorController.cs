using System.Web.Mvc;

namespace CourseProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdministratorController : Controller
    {
        public ActionResult AdministratorMain()
        {
            return View();
        }
    }
}