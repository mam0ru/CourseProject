using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Microsoft.AspNet.Identity.Owin;
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

        private readonly IFormulaRepository formulaRepository;

        private readonly ITagRepository tagRepository;

        private ApplicationUserManager userManager;

        private Account account = new Account("dkfntkp0r", "284111675587747", "shagM6LcW1MFmkWU60j2L9FWPps");

        public ExerciseController(IExerciseRepository exerciseRepository, ICategoryRepository categoryRepository, IPictureRepository pictureRepository, IAnswerRepository answerRepository, ICommentRepository commentRepository, ITagRepository tagRepository, IFormulaRepository formulaRepository, ApplicationUserManager userManager)
        {
            this.exerciseRepository = exerciseRepository;
            this.categoryRepository = categoryRepository;
            this.pictureRepository = pictureRepository;
            this.answerRepository = answerRepository;
            this.commentRepository = commentRepository;
            this.tagRepository = tagRepository;
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
        public ActionResult CreateExercise()
        {
            ViewBag.categories = categoryRepository.Get().Select(category => category.Text);
            return View();
        }

        [HttpPost]
        public ActionResult CreateExercise(ExerciseCreateViewModel model)
        {
            var exercise = InitializExercise(model);
            exerciseRepository.Update(exercise);
            return RedirectToAction("Index","Home");
        }

        private Exercise InitializExercise(ExerciseCreateViewModel model)
        {
            var exercise = new Exercise();
            exercise.Active = true;
            exercise.Author = userManager.FindById(Request.LogonUserIdentity.GetUserId());
            var categ = categoryRepository.Get(category => category.Text == model.Category).First();
            exercise.Category = categ;
            exercise.Name = model.Name;
            exercise.Text = model.Text;
            exercise.TriesOfAnswers = 0;
            exerciseRepository.Insert(exercise);
            ICollection<Answer> answers = new Collection<Answer>();
            if (model.Answers != null)
            {
                foreach (String ans in model.Answers.Split(','))
                {
                    Answer answer = new Answer();
                    answer.Text = ans;
                    answer.Task = exercise;
                    answers.Add(answer);
                    answerRepository.Insert(answer);
                }
                exercise.Answers = answers;                
            }
            exercise.Answers = answers;

            if (model.Formulas != null)
            {
                ICollection<Formula> formulas = new Collection<Formula>();
                foreach (String eq in model.Formulas.Split(','))
                {
                    Formula equation = new Formula();
                    equation.Path = eq;
                    equation.Task = exercise;
                    formulas.Add(equation);
                    formulaRepository.Insert(equation);
                }
                exercise.Formulas = formulas;
            }

            if (model.Pictures != null)
            {
                ICollection<Picture> pictures = new Collection<Picture>();
                foreach (String imageSource in model.Pictures.Split(','))
                {
                    Picture picture = new Picture();
                    picture.Path = imageSource;
                    picture.Task = exercise;
                    pictures.Add(picture);
                    pictureRepository.Insert(picture);
                }
                exercise.Pictures = pictures;   
            }
            if (model.Tags != null)
            {
                ICollection<Tag> addingTags = new Collection<Tag>();
                foreach (String tag in model.Tags.Split(','))
                {
                    IEnumerable<Tag> tags = tagRepository.Get();
                    Tag tagTemp = tags.FirstOrDefault(tag1 => tag1.Text == tag);

                    if (tagTemp == null)
                    {
                        tagTemp = new Tag();
                        tagTemp.Text = tag;
                        tagRepository.Insert(tagTemp);
                    }
                    addingTags.Add(tagTemp);
                }
                exercise.Tags = addingTags;
            }
            //exercise.Graphs = model.Graphs;
            //exercise.Videos = model.Videos;
            return exercise;
        }

        [HttpGet]
        public ActionResult ShowExercise(int id)
        {
            var exercise = exerciseRepository.GetByID(id);
            return View(exercise);//exerciseRepository.GetByID(id));
        }

        [HttpGet]
        public ActionResult EditExercise(int id)
        {
            var exercise = exerciseRepository.GetByID(id);
            return View(exercise);//exerciseRepository.GetByID(id));
        }

        [HttpGet]
        public ActionResult DeleteExercise(int id)
        {
            var exercise = exerciseRepository.GetByID(id);
           exerciseRepository.Delete(exercise);
           return RedirectToAction("AdministratorMain","Administrator");
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
            var newAnswer = new Answer();
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
            var newComment = new Comment();
            newComment.Target = exercise;
            newComment.Text = comment;
            newComment.Author = userManager.FindById(Request.LogonUserIdentity.GetUserId());
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
            var cloudinary = new Cloudinary(account);
            var image = Request.Files["imageupload"];
            var param = new ImageUploadParams()
            {
                File = new FileDescription(image.FileName, image.InputStream)
            };

            var uploadResult = cloudinary.Upload(param);
            var uplPath = uploadResult.Uri.AbsoluteUri;
            var uploadedPicture = new Picture();
            uploadedPicture.Path = uplPath;
            uploadedPicture.Name = uploadResult.PublicId;
            pictureRepository.Insert(uploadedPicture);
            return Json(new {path = uplPath, });
        }

        public ActionResult TagAutocompliteSearch(string term)
        {
            var models = tagRepository.Get().Where(tag => tag.Text.Contains(term)).Select(tag => new{value = tag.Text}).Distinct();
            return Json(models, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCategoties()
        {
            var categories = categoryRepository.Get().Select(category => new {value = category.Text});
            return Json(categories,JsonRequestBehavior.AllowGet);
        }

    }
}