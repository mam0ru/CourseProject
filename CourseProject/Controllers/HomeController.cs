using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseProject.Repository;
using CourseProject.Repository.Interfaces;

namespace CourseProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApplicationUserRepository applicationUserRepository;

        private readonly IExerciseRepository exerciseRepository;

        private readonly ITagRepository tagRepository;

        public HomeController(IApplicationUserRepository applicationUserRepository, IExerciseRepository exerciseRepository, ITagRepository tagRepository)
        {
            this.applicationUserRepository = applicationUserRepository;
            this.tagRepository = tagRepository;
            this.exerciseRepository = exerciseRepository;
        }

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

        [HttpPost]
        public ActionResult Search(string search)
        {
            int i = 0;
            i++;
            return View("Index");//"Rating", MvcApplication.dataBase.UserRepository.Get().OrderBy(user => user.RightAnswers.Count()));
        }

        /*[HttPost]
        public ActionResult Search()
        {
            return View("Rating", MvcApplication.dataBase.UserRepository.Get().OrderBy(user => user.RightAnswers.Count()));
        }*/

        public ActionResult SidePanel()
        {
            return PartialView("_SidePanelPartial", exerciseRepository.Get());
        }

        public ActionResult TagsOutput()
        {
            return PartialView("_TagCloudPartial",tagRepository.Get().OrderByDescending(tag => tag.Task.Count()));
        }
    }
}