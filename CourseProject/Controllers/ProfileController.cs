﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseProject.Models;
using Microsoft.AspNet.Identity;

namespace CourseProject.Controllers
{
     [Authorize(Roles = "user")]
    public class ProfileController : Controller
    {
        // GET: Profile
        [HttpGet]
        public ActionResult MyProfile()
        {
            return View(MvcApplication.dataBase.UserRepository.GetByID(User.Identity.GetUserId()));
        }

        public ActionResult MakeActiveUnactive(int id, bool isActive)
        {
            MvcApplication.dataBase.ExerciseRepository.dbSet.Single(exersise => exersise.Id == id).Active = !isActive;
           // context.Exercises.
            return View(MvcApplication.dataBase.ExerciseRepository.dbSet);
        }

        [HttpGet]
        public ActionResult ShowProfile()
        {
            return View();//MvcApplication.dataBase.UserRepository.GetByID(User.Identity.GetUserId()));
        }

        [HttpPost]
        public ActionResult ShowProfile(int id)
        {
            return View(MvcApplication.dataBase.UserRepository.GetByID(id));
        }
        /*[HttpPost]
        public ActionResult MyProfile()
        {
            return RedirectToActionPermanent(context.Exercises);
        }*/
    }
}