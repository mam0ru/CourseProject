using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using CourseProject.Models;
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

        public ActionResult SidePanel()
        {
            return PartialView("_SidePanelPartial", exerciseRepository.Get());
        }

        public ActionResult TagsOutput()
        {
            return PartialView("_TagCloudPartial", tagRepository.Get().OrderByDescending(tag => tag.Task.Count()));
        }

        [HttpPost]
        public ActionResult SendMail(string SenderAddress, string Message)
        {
            string username = "course.project.itr@gmail.com";
            string password = "project123456";
            NetworkCredential loginInfo = new NetworkCredential(username, password);
            MailMessage msg = new MailMessage();

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 25);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = loginInfo;

            string message = " (" + SenderAddress + ") has a message for you:<br /><br />" + Message;

            try
            {
                msg.From = new MailAddress("course.project.itr@gmail.com");
                msg.To.Add(new MailAddress(SenderAddress));
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
    }
}