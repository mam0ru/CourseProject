using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SetTheme(String theme = "white")
        {
            List<string> themes = new List<string>() { "white", "dark" };
            if (!themes.Contains(theme))
            {
                theme = "white";
            }
            HttpCookie cookie = Request.Cookies["theme"];
            if (cookie != null)
                cookie.Value = theme;
            else
            {
                cookie = new HttpCookie("theme");
                cookie.HttpOnly = false;
                cookie.Value = theme;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            String redirectTo = Request.UrlReferrer.AbsolutePath;
            return Redirect(redirectTo);
        }

        public ActionResult Rating()
        {
           
            return View("Rating", MvcApplication.dataBase.UserRepository.Get().OrderBy(user => user.RightAnswers.Count()));
        }
        [HttpGet]
        public ActionResult Search()
        {

            return View("Index");//"Rating", MvcApplication.dataBase.UserRepository.Get().OrderBy(user => user.RightAnswers.Count()));
        }
        /*[HttPost]
        public ActionResult Search()
        {
            return View("Rating", MvcApplication.dataBase.UserRepository.Get().OrderBy(user => user.RightAnswers.Count()));
        }*/
    }
}