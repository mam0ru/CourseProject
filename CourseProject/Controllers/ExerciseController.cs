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
        public ActionResult SendAnswer(int id,string answer)
        {
            var exercise = exerciseRepository.GetByID(id);
            var answers = answerRepository.Get().Select(localAnswer => localAnswer.Text);
            bool answerFound = false;
            for (int i = 0; i < answers.Count() || !answerFound; i++)
            {
                if (answers.ElementAt(i) == answer)
                {
                    answerFound = true;
                    exercise.RightAnsweredUsers.Add(userManager.FindById(User.Identity.GetUserId()));
                    userManager.FindById(User.Identity.GetUserId()).RightAnswers.Add(exercise);
                    exerciseRepository.Update(exercise);
                    TempData["alertMessage"] = "You answered right!";
                }
            }
           
           return RedirectToAction("ShowExercise",id);
        }

        [HttpPost]
        public ActionResult CreateExercise(ExerciseCreateViewModel model)
        {
            var exercise = InitializExercise(model);
            exerciseRepository.Update(exercise);
            userManager.FindById(User.Identity.GetUserId()).Exercises.Add(exercise);
            return RedirectToAction("Index","Home");
        }

        private Exercise InitializExercise(ExerciseCreateViewModel model)
        {
            var exercise = new Exercise();
            exercise.Active = true;
            exercise.Author = userManager.FindById(User.Identity.GetUserId());
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
            ViewBag.IsRightAnweredUser = true;
            if(exercise.RightAnsweredUsers.Contains(userManager.FindById(User.Identity.GetUserId())))
            {
                 ViewBag.IsRightAnweredUser = true;
            }
            else
            {
                 ViewBag.IsRightAnweredUser = false;
            }
            return View(exercise);
        }

        [HttpGet]
        public ActionResult EditExercise(int id)
        {
            var exercise = exerciseRepository.GetByID(id);
            EditExerciseViewModel model = new EditExerciseViewModel();
            model.Exercise = exercise;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditExercise(EditExerciseViewModel model)
        {
            Exercise exercise = exerciseRepository.GetByID(model.Exercise.Id);
            exercise.Name = model.Name;
            exercise.Text = model.Text;
            List<String> oldAnswers = exercise.Answers.Select(answer => answer.Text).ToList();
            List<String> newAnswers = model.Answers.Split(',').ToList();
            foreach (String oldAnswer in oldAnswers)
            {
                if (!newAnswers.Contains(oldAnswer))
                {
                    exercise.Answers.Remove(exercise.Answers.First(ans => ans.Text == oldAnswer));
                }
            }
            foreach (String newAnswer in newAnswers)
            {
                if (!oldAnswers.Contains(newAnswer))
                {
                    Answer ans = new Answer();
                    ans.Text = newAnswer;
                    answerRepository.Insert(ans);
                    exercise.Answers.Add(ans);
                }
            }

            List<String> oldTags = exercise.Tags.Select(tag => tag.Text).ToList();
            List<String> newTags = model.Tags.Split(',').ToList();
            foreach (String oldTag in oldTags)
            {
                if (!newTags.Contains(oldTag))
                {
                    exercise.Tags.Remove(exercise.Tags.First(tag => tag.Text == oldTag));
                }
            }
            foreach (String newTag in newTags)
            {
                if (!oldTags.Contains(newTag))
                {
                    Tag tag = new Tag();
                    tag.Text = newTag;
                    tagRepository.Insert(tag);
                    exercise.Tags.Add(tag);
                }
            }

            return RedirectToAction("Index","Home");
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
            ViewBag.Tag = tag;
            var exercises = tagRepository.Get().First(localTag => localTag.Text == tag).Task;
            return View("ShowExercisesWithTag",exercises);
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