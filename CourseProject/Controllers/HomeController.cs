using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using CourseProject.Repository;
using CourseProject.Repository.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using MultilingualSite.Filters;

namespace CourseProject.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        private readonly IApplicationUserRepository applicationUserRepository;

        private readonly IExerciseRepository exerciseRepository;

        private readonly ITagRepository tagRepository;


        private ApplicationUserManager userManager;

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

        public HomeController(IApplicationUserRepository applicationUserRepository, IExerciseRepository exerciseRepository, ITagRepository tagRepository,ApplicationUserManager userManager)
        {
            this.applicationUserRepository = applicationUserRepository;
            this.tagRepository = tagRepository;
            this.exerciseRepository = exerciseRepository;
            this.userManager = userManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SetTheme(String theme)
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

        public ActionResult SidePanel()
        {
            return PartialView("_SidePanelPartial", exerciseRepository.Get());
        }

        public ActionResult TagsOutput()
        {
            return PartialView("_TagCloudPartial", tagRepository.Get().OrderByDescending(tag => tag.Task.Count()));
        }

        [HttpPost]
        public ActionResult SendMail(string id, string Message)
        {
            string username = "course.project.itr@gmail.com";
            string password = "project123456";
            var loginInfo = new NetworkCredential(username, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 25);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = loginInfo;
            String sendTo = exerciseRepository.GetByID(Convert.ToInt32(id)).Author.Email;
            string message = "User find mistake in your task.<br />" + Message;
            try
            {
                msg.From = new MailAddress("course.project.itr@gmail.com");
                msg.To.Add(new MailAddress(sendTo));
                msg.Subject = "Web Message";
                msg.Sender = new MailAddress("course.project.itr@gmail.com");
                msg.Body = message;
                msg.IsBodyHtml = true;
                smtpClient.Send(msg);
                return Content("Your message was sent successfully!");
            }
            catch (Exception)
            {
                return Content("There was an error... please try again.");
            }
        }

         public ActionResult ChangeCulture(string lang)
        {
            var cultures = new List<string>() {"ru", "en"};
            if (!cultures.Contains(lang))
            {
                lang = "en";
            }
            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = lang;
            else
            {
                cookie = new HttpCookie("lang");
                cookie.HttpOnly = false;
                cookie.Value = lang;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            string returnUrl = Request.UrlReferrer.AbsolutePath;
            return Redirect(returnUrl);
        }
    }
}
