﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using CourseProject.Models;
using CourseProject.View_Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using MultilingualSite.Filters;


namespace CourseProject.Controllers
{
    [Culture]
    [Authorize(Roles = "admin")]

    public class AdministratorController : Controller
    {
        private ApplicationUserManager userManager;

        public AdministratorController(ApplicationUserManager userManager)
        {
            this.UserManager = userManager;
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
                userForAdmin.Blocked = user.LockoutEnabled;
                userForAdmin.Deleted = false;
                userForAdmin.DroppedPassword = false;
                userForAdmin.Email = user.Email;
                userForAdmin.Name = user.UserName;
                userForAdmin.Rating = user.Rating;
                userForAdmin.SolvedExercises = user.RightAnswers;
                userForAdmin.UsersExercises = user.Exercises;
                userForAdmin.Id = user.Id;
                users.Add(userForAdmin);
            }
            return View(users);
        }

        [HttpGet]
        public ActionResult AdministratorMain(List<UserForAdministratorMainViewModel> users)
        {
            foreach (var user in users)
            {
                bool isUserChanged = false;
                var applicationUser = UserManager.FindById(user.Id);
                if (user.Deleted)
                {
                    UserManager.Delete(applicationUser);
                }
                else
                {
                    bool a = UserManager.IsLockedOut(applicationUser.Id);
                    if (UserManager.IsLockedOut(applicationUser.Id) != user.Blocked)
                    {
                        UserManager.SetLockoutEnabled(applicationUser.Id, user.Blocked);
                        if (user.Blocked)
                        {
                            DateTime date = DateTime.Now;
                            DateTime blockEndDate = new DateTime(date.Year, date.Month + 1, date.Day);
                            UserManager.SetLockoutEndDate(applicationUser.Id, blockEndDate);
                        }
                        isUserChanged = true;
                    }
                    if (user.DroppedPassword)
                    {
                        String newPassword = "123456";
                        ApplicationUser cUser = UserManager.FindById(applicationUser.Id);
                        String hashedNewPassword = UserManager.PasswordHasher.HashPassword(newPassword);
                        var store = new UserStore<ApplicationUser>();
                        store.SetPasswordHashAsync(cUser, hashedNewPassword);
                        //UserManager.SendEmail(applicationUser.Id, "Your password was dropped and replaced", "Your new password is 123456");
                        isUserChanged = true;
                    }
                    if ((UserManager.IsInRole(applicationUser.Id, "admin") != user.Admin))
                    {
                        if (user.Admin)
                        {
                            UserManager.AddToRole(applicationUser.Id, "admin");
                        }
                        else
                        {
                            UserManager.RemoveFromRole(applicationUser.Id, "admin");
                            UserManager.Update(applicationUser);
                        }
                        isUserChanged = true;
                    }
                    if (isUserChanged)
                    {
                        UserManager.Update(applicationUser);
                    }
                    user.DroppedPassword = false;
                }
            }
            var newUsers = new List<UserForAdministratorMainViewModel>();
            foreach (var user in UserManager.Users)
            {
                var userForAdmin = new UserForAdministratorMainViewModel
                {
                    Admin = UserManager.IsInRole(user.Id, "admin"),
                    Blocked = user.LockoutEnabled,
                    Deleted = false,
                    DroppedPassword = false,
                    Email = user.Email,
                    Name = user.UserName,
                    Rating = user.Rating,
                    SolvedExercises = user.RightAnswers,
                    UsersExercises = user.Exercises,
                    Id = user.Id
                };
                newUsers.Add(userForAdmin);
            }
            return RedirectToAction("AdministratorMain");
        }
    }
}