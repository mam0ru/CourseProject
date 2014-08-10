using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using CloudinaryDotNet.Actions;
using CourseProject.Models;
using CourseProject.Repository;
using CourseProject.Repository.Implementation;
using CourseProject.Repository.Interfaces;
using CourseProject.View_Models;
using CloudinaryDotNet;
using Microsoft.AspNet.Identity;
using Ninject;

namespace CourseProject.Controllers
{
    [Authorize]
    public class ExerciseController : Controller
    {
        private readonly IExerciseRepository exerciseRepository;

        private readonly ICategoryRepository categoryRepository;

        private readonly IPictureRepository pictureRepository;

        private readonly IAnswerRepository answerRepository;

        private readonly ICommentRepository commentRepository;

        private readonly IApplicationUserRepository applicationUserRepository;

        private Account account = new Account("dkfntkp0r", "284111675587747", "shagM6LcW1MFmkWU60j2L9FWPps");

        public ExerciseController(IExerciseRepository exerciseRepository, ICategoryRepository categoryRepository, IPictureRepository pictureRepository, IAnswerRepository answerRepository,  ICommentRepository commentRepository, IApplicationUserRepository applicationUserRepository)
        {
            this.exerciseRepository = exerciseRepository;
            this.categoryRepository = categoryRepository;
            this.pictureRepository = pictureRepository;
            this.answerRepository = answerRepository;
            this.commentRepository = commentRepository;
            this.applicationUserRepository = applicationUserRepository;
        }

        [HttpGet]
        public ActionResult CreateExercise()
        {
            ViewBag.categories = categoryRepository.Get().Select(category => category.Text);
            return View();
        }

        [HttpPost]
        public ActionResult CreateExercise(ExerciseCreateViewModel model)
        {
            Exercise exercise = InitializExercise(model);
            exerciseRepository.Insert(exercise);
            return RedirectToAction("Index","Home");
        }

        private Exercise InitializExercise(ExerciseCreateViewModel model)
        {
            Exercise exercise = new Exercise();
            exercise.Active = true;
            exercise.Answers = model.Answers;
            exercise.Author = applicationUserRepository.GetByID(Request.LogonUserIdentity.GetUserId());
            // TODO: add category in view
            Category category = null;//= categoryRepository.Get(category => category.Text == model.Category).First();
            exercise.Category = category;
            exercise.Formulas = model.Formulas;
            exercise.Graphs = model.Graphs;
            exercise.Name = model.Name;
            exercise.Pictures = model.Pictures;
            exercise.Text = model.Text;
            exercise.Tags = model.Tags;
            exercise.TriesOfAnswers = 0;
            exercise.Videos = model.Videos;
            return exercise;
        }

        [HttpGet]
        public ActionResult ShowExercise(int id)
        {
            return View(exerciseRepository.GetByID(id));
        }

        [HttpPost]
        public ActionResult ShowExercisesWithTag(string tag)
        {
            return RedirectToAction("Index", "Home"); //"ShowExercise", "Exersise",);
        }

        [HttpGet]
        public ActionResult ShowExercisesWithTag()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddAnswer()
        {
            //MvcApplication.dataBase.ExerciseRepository.dbSet.Single(exersise => exersise.Id == id).Active = !isActive;
            // context.Exercises.
            return RedirectToAction("MyProfile", "Profile");
        }

        [HttpPost]
        public ActionResult AddAnswer(int id, string answer)
        {
            var exercise = exerciseRepository.GetByID(id);
            Answer newAnswer = new Answer();
            newAnswer.Task = exercise;
            newAnswer.Text = answer;
            answerRepository.Update(newAnswer);
            exerciseRepository.Update(exercise);
            return RedirectToAction("MyProfile", "Profile");
        }

        [HttpPost]
        public ActionResult AddComment(int id, string comment)
        {
            var exercise = exerciseRepository.GetByID(id);
            Comment newComment = new Comment();
            newComment.Target = exercise;
            newComment.Text = comment;
            newComment.Author = applicationUserRepository.GetByID(Request.LogonUserIdentity.GetUserId());
            commentRepository.Update(newComment);
            exerciseRepository.Update(exercise);
            return RedirectToAction("MyProfile", "Profile");
        }

        [HttpPost]
        public ActionResult WriteToAuthor(int id, string text)
        {
            var exercise = exerciseRepository.GetByID(id);
           //TODO: email
            return RedirectToAction("MyProfile", "Profile");
        }

        [HttpPost]
        public ActionResult UploadImage()
        {
            Cloudinary cloudinary = new Cloudinary(account);
            HttpPostedFileBase image = Request.Files["imageupload"];
            var param = new ImageUploadParams()
            {
                File = new FileDescription(image.FileName, image.InputStream)
            };

            var uploadResult = cloudinary.Upload(param);
            var uplPath = uploadResult.Uri.AbsoluteUri;
            Picture uploadedPicture = new Picture();
            uploadedPicture.Path = uplPath;
            uploadedPicture.Name = uploadResult.PublicId;
            pictureRepository.Insert(uploadedPicture);
            return Json(new {path = uplPath, });
        }

    }
}