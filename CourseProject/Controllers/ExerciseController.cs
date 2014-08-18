using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using CloudinaryDotNet.Actions;
using CourseProject.Models;
using CourseProject.Repository;
using CourseProject.Repository.Implementation;
using CourseProject.Repository.Interfaces;
using CourseProject.ViewModels;
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

        private readonly IEquationRepository equationRepository;

        private readonly ITagRepository tagRepository;

        private readonly IEvaluationRepository evaluationRepository;

        private readonly IGraphRepository graphRepository;

        private readonly IVideoRepository videoRepository;

        private ApplicationUserManager userManager;

        private Account account = new Account("dkfntkp0r", "284111675587747", "shagM6LcW1MFmkWU60j2L9FWPps");

        public ExerciseController(IExerciseRepository exerciseRepository,
            ICategoryRepository categoryRepository,
            IPictureRepository pictureRepository,
            IAnswerRepository answerRepository,
            ICommentRepository commentRepository,
            ITagRepository tagRepository,
            IEvaluationRepository evaluationRepository,
            IEquationRepository equationRepository,
            ApplicationUserManager userManager,
            IGraphRepository graphRepository,
            IVideoRepository videoRepository)
        {
            this.exerciseRepository = exerciseRepository;
            this.categoryRepository = categoryRepository;
            this.pictureRepository = pictureRepository;
            this.answerRepository = answerRepository;
            this.commentRepository = commentRepository;
            this.evaluationRepository = evaluationRepository;
            this.tagRepository = tagRepository;
            this.userManager = userManager;
            this.equationRepository = equationRepository;
            this.graphRepository = graphRepository;
            this.videoRepository = videoRepository;
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

        [HttpGet]
        public ActionResult AddEvaluation(int id, string evaluationButton)
        {
            //все , что закомменчено - второй способ
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            Exercise exercise = exerciseRepository.GetByID(id);
            if (exercise.Author.Id != user.Id)
            {
                Evaluation previousEvaluation = evaluationRepository.Get().First(localEvaluation => localEvaluation.Target.Id == id && localEvaluation.User == user);
                //var previousEvaluation = exercise.Evaluations.First(localEvaluation => localEvaluation.User == user);
                if (previousEvaluation != null)
                {
                    bool type = previousEvaluation.Type;
                    switch (evaluationButton)
                    {
                        case "like":
                            if (!type)
                            {
                                previousEvaluation.Type = true;
                                /*var newEvaluation = previousEvaluation;
                        newEvaluation.Type = true;
                        user.Exercises.Remove(exercise);
                        exercise.Evaluations.Remove(previousEvaluation);
                        exercise.Evaluations.Add(newEvaluation);
                        exerciseRepository.Update(exercise);
                        user.Exercises.Add(exercise);  
                        userManager.UpdateAsync(user);
                        */
                                evaluationRepository.Update(previousEvaluation);
                            }
                            return RedirectToAction("ShowExercise", id);
                        case "dislike":
                            if (type)
                            {
                                previousEvaluation.Type = false;
                                /*var newEvaluation = previousEvaluation;
                        newEvaluation.Type = false;
                        user.Exercises.Remove(exercise);
                        exercise.Evaluations.Remove(previousEvaluation);
                        exercise.Evaluations.Add(newEvaluation);
                        exerciseRepository.Update(exercise);
                        user.Exercises.Add(exercise);  
                        userManager.UpdateAsync(user);
                        */
                                evaluationRepository.Update(previousEvaluation);
                            }
                            return RedirectToAction("ShowExercise", id);
                        default:
                            return RedirectToAction("ShowExercise", id);
                    }
                }
            }
            return RedirectToAction("ShowExercise", id);
        }

        [HttpPost]
        public ActionResult AddComment(AddCommentViewModel model)
        {
            ApplicationUser user = userManager.FindById(model.UserId);
            Exercise exercise = exerciseRepository.GetByID(model.ExerciseId);
            Comment newComment = new Comment();
            newComment.Target = exercise;
            newComment.Text = model.Text;
            newComment.Author = user;
            commentRepository.Insert(newComment);
            exercise.Comments.Add(newComment);
            exerciseRepository.Update(exercise);
            return RedirectToAction("ShowExercise", "Exercise",new {id = model.ExerciseId});
        }

        [HttpGet]
        public ActionResult AddComment(int id)
        {
            var user = userManager.FindById(User.Identity.GetUserId());
            AddCommentViewModel model = new AddCommentViewModel();
            model.ExerciseId = id;
            model.ImagePath = user.ImagePath;
            model.UserId = user.Id;
            model.UserName = user.UserName;
            return PartialView("_AddCommentPartial", model);
        }

        [HttpPost]
        public ActionResult SendAnswer(int id, string answer)
        {
            //simplify
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            Exercise exercise = exerciseRepository.GetByID(id);
            IEnumerable<string> answers = answerRepository.Get().Select(localAnswer => localAnswer.Text);
            bool answerFound = false;
            for (int i = 0; i < answers.Count() || !answerFound; i++)
            {
                if (answers.ElementAt(i) == answer)
                {
                    answerFound = true;
                    exercise.RightAnsweredUsers.Add(user);
                    exerciseRepository.Update(exercise);
                    user.RightAnswers.Add(exercise);
                    userManager.UpdateAsync(user);
                    TempData["alertMessage"] = "You answered right!";
                }
            }

            return RedirectToAction("ShowExercise", id);
        }

        [HttpPost]
        public ActionResult CreateExercise(ExerciseCreateViewModel model)
        {
            Exercise exercise = InitializExercise(model);
            exerciseRepository.Update(exercise);
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            user.Exercises.Add(exercise);
            userManager.Update(user);
            exerciseRepository.Update(exercise);
            return RedirectToAction("MyProfile", "Profile");
        }

        private Exercise InitializExercise(ExerciseCreateViewModel model)
        {
            Exercise exercise = new Exercise();
            exercise.Active = true;
            exercise.Author = userManager.FindById(User.Identity.GetUserId());
            Category categ = categoryRepository.Get(category => category.Text == model.Category).First();
            exercise.Category = categ;
            exercise.Name = model.Name;
            exercise.Text = model.Text;
            exercise.TriesOfAnswers = 0;
            exerciseRepository.Insert(exercise);
            if (model.Answers != null)
            {
                ICollection<Answer> answers = new Collection<Answer>();
                List<String> modelAnswers = System.Web.Helpers.Json.Decode<List<String>>(model.Answers);
                foreach (String ans in modelAnswers)
                {
                    Answer answer = new Answer();
                    answer.Text = ans;
                    answer.Task = exercise;
                    answers.Add(answer);
                    answerRepository.Insert(answer);
                }
                exercise.Answers = answers;
            }

            if (model.Graphs != null)
            {
                ICollection<Graph> graphs = new Collection<Graph>();
                List<String> modelGraphs = System.Web.Helpers.Json.Decode<List<String>>(model.Graphs);
                foreach (String info in modelGraphs)
                {
                    Graph graph = new Graph();
                    graph.Path = info;
                    graph.Task = exercise;
                    graphs.Add(graph);
                    graphRepository.Insert(graph);
                }
                exercise.Graphs = graphs;
            }

            if (model.Pictures != null)
            {
                ICollection<Picture> pictures = new Collection<Picture>();
                List <String> pictureSources = System.Web.Helpers.Json.Decode<List<String>>(model.Pictures); 
                foreach (String pictureSource in pictureSources)
                {
                    Picture picture = new Picture();
                    picture.Path = pictureSource;
                    picture.Task = exercise;
                    pictures.Add(picture);
                    pictureRepository.Insert(picture);
                }
                exercise.Pictures = pictures;
            }
            if (model.Tags != null)
            {
                List<String> modelTags = System.Web.Helpers.Json.Decode<List<String>>(model.Tags);
                ICollection<Tag> addingTags = new Collection<Tag>();
                IEnumerable<Tag> tags = tagRepository.Get();
                foreach (String tag in modelTags)
                {
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

            if (model.Formulas != null)
            {
                ICollection<Equation> equations = new Collection<Equation>();
                List<String> modelFormulas = System.Web.Helpers.Json.Decode<List<String>>(model.Formulas);
                foreach (String eq in modelFormulas)
                {
                    Equation equation = new Equation();
                    equation.Path = eq;
                    equation.Task = exercise;
                    equations.Add(equation);
                    equationRepository.Insert(equation);
                }
                exercise.Equations = equations;
            }
            if (model.Videos != null)
            {
                ICollection<Video> videos = new Collection<Video>();
                List<String> modelVideos = System.Web.Helpers.Json.Decode<List<String>>(model.Videos);
                foreach (string modelVideo in modelVideos)
                {
                    Video video = new Video();
                    video.Path = modelVideo;
                    video.Task = exercise;
                    videos.Add(video);
                    videoRepository.Insert(video);
                }
                exercise.Videos = videos;
            }
            return exercise;
        }

        [HttpGet]
        public ActionResult ShowExercise(int id)
        {
            Exercise exercise = exerciseRepository.GetByID(id);
            ViewBag.IsRightAnweredUser = true;
            if (exercise.RightAnsweredUsers.Contains(userManager.FindById(User.Identity.GetUserId())))
            {
                ViewBag.IsRightAnweredUser = true;
            }
            //else
            //{
               // ViewBag.IsRightAnweredUser = false;
           // }
            return View(exercise);
        }

        [HttpGet]
        public ActionResult EditExercise(int id)
        {
            Exercise exercise = exerciseRepository.GetByID(id);
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
            List<String> newAnswers = System.Web.Helpers.Json.Decode<List<String>>(model.Answers);
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
            List<String> newTags = System.Web.Helpers.Json.Decode<List<String>>(model.Tags);
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
            if (model.Equations != null)
            {
                ICollection<Equation> equations = new Collection<Equation>();
                List<String> oldEquations = exercise.Equations.Select(equation => equation.Path).ToList();
                List<String> newEquations = System.Web.Helpers.Json.Decode<List<String>>(model.Equations);

                foreach (String oldEquation in oldEquations)
                {
                    if (!newEquations.Contains(oldEquation))
                    {
                        Equation eq = exercise.Equations.First(equation => equation.Path == oldEquation);
                        exercise.Equations.Remove(eq);
                    }
                }

                foreach (String newEquation in newEquations)
                {
                    if (!oldEquations.Contains(newEquation))
                    {
                        Equation equation = new Equation();
                        equation.Path = newEquation;
                        equation.Task = exercise;
                        equationRepository.Insert(equation);
                    }
                }
            }

            if (model.Graphs != null)
            {
                ICollection<Graph> graphs = new Collection<Graph>();
                List<String> oldGraphs = exercise.Graphs.Select(graph => graph.Path).ToList();
                List<String> newGraphs = System.Web.Helpers.Json.Decode<List<String>>(model.Graphs);

                foreach (String oldGraph in oldGraphs)
                {
                    if (!newGraphs.Contains(oldGraph))
                    {
                        Graph graph = exercise.Graphs.First(g => g.Path == oldGraph);
                        exercise.Graphs.Remove(graph);
                        graphRepository.Delete(graph);
                    }
                }

                foreach (String newGraph in newGraphs)
                {
                    if (!oldGraphs.Contains(newGraph))
                    {
                        Graph graph = new Graph();
                        graph.Path = newGraph;
                        graph.Task = exercise;
                        graphRepository.Insert(graph);
                    }
                }
            }
            else
            {
                ICollection<Graph> graphs = exercise.Graphs;
                foreach (Graph graph in graphs)
                {
                    exercise.Graphs.Remove(graph);
                    graphRepository.Delete(graph);
                }
            }

            if (model.Videos != null)
            {
                ICollection<Video> videos = new Collection<Video>();
                List<String> oldVideos = exercise.Videos.Select(v => v.Path).ToList();
                List<String> newVideos = System.Web.Helpers.Json.Decode<List<String>>(model.Videos);

                foreach (String oldVideo in oldVideos)
                {
                    if (!newVideos.Contains(oldVideo))
                    {
                        Video video = exercise.Videos.First(v => v.Path == oldVideo);
                        exercise.Videos.Remove(video);
                        videoRepository.Delete(video);
                    }
                }

                foreach (String newVideo in newVideos)
                {
                    if (!oldVideos.Contains(newVideo))
                    {
                        Video video = new Video();
                        video.Path = newVideo;
                        video.Task = exercise;
                        videoRepository.Insert(video);
                    }
                }
            }
            else
            {
                ICollection<Video> videos = exercise.Videos;
                if (videos != null)
                {
                    foreach (Video video in videos)
                    {
                        exercise.Videos.Remove(video);
                        videoRepository.Delete(video);
                    }   
                }
            }
            if (model.Pictures != null)
            {
                ICollection<Picture> pictures = new Collection<Picture>();
                List<String> oldPictures= exercise.Pictures.Select(p => p.Path).ToList();
                List<String> newPictures = System.Web.Helpers.Json.Decode<List<String>>(model.Pictures);

                foreach (String oldPicture in oldPictures)
                {
                    if (!newPictures.Contains(oldPicture))
                    {
                        Picture picture = exercise.Pictures.First(p => p.Path == oldPicture);
                        exercise.Pictures.Remove(picture);
                        pictureRepository.Delete(picture);
                    }
                }

                foreach (String newPicture in newPictures)
                {
                    if (!oldPictures.Contains(newPicture))
                    {
                        Picture picture = new Picture();
                        picture.Path = newPicture;
                        picture.Task = exercise;
                        pictureRepository.Insert(picture);
                    }
                }
            }
            else
            {
                var pictures = exercise.Pictures;
                if (pictures != null)
                {
                    foreach (var picture in pictures)
                    {
                        exercise.Pictures.Remove(picture);
                        pictureRepository.Delete(picture);
                    }                    
                }
            }
            exerciseRepository.Update(exercise);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult DeleteExercise(int id)
        {
            Exercise exercise = exerciseRepository.GetByID(id);
            exerciseRepository.Delete(exercise);
            return RedirectToAction("AdministratorMain", "Administrator");
        }

        [HttpGet]
        public ActionResult ShowExercisesWithTag(string tag)
        {
            ViewBag.Tag = tag;
            var targetTag = tagRepository.Get().First(localTag => localTag.Text == tag);
            IEnumerable<Exercise> exercises = targetTag.Task.Select(exercise => exercise);
            return View(exercises);
        }

        //[HttpGet]
        //public ActionResult ShowExercisesWithTag()
        //{
        //    return View();
        //}

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
            Exercise exercise = exerciseRepository.GetByID(id);
            Answer newAnswer = new Answer();
            newAnswer.Task = exercise;
            newAnswer.Text = answer;
            answerRepository.Update(newAnswer);
            exerciseRepository.Update(exercise);
            return RedirectToAction("MyProfile", "Profile");
        }

        [HttpPost]
        public ActionResult WriteToAuthor(int id, string text)
        {
            Exercise exercise = exerciseRepository.GetByID(id);
            //TODO: email
            return RedirectToAction("MyProfile", "Profile");
        }

        [HttpPost]
        public ActionResult UploadImage()
        {
            Cloudinary cloudinary = new Cloudinary(account);
            HttpPostedFileBase image = Request.Files["imageupload"];
            ImageUploadParams param = new ImageUploadParams()
            {
                File = new FileDescription(image.FileName, image.InputStream)
            };

            ImageUploadResult uploadResult = cloudinary.Upload(param);
            string uplPath;
            if (uploadResult.Width > 880)
            {
                uplPath = uploadResult.Uri.AbsoluteUri;
                string label = "upload/";
                int insertIndex = uplPath.IndexOf(label) + label.Length;
                string setImageSize = "c_scale,w_880/";
                uplPath = uplPath.Insert(insertIndex,setImageSize);
            }
            else
            {
                uplPath = uploadResult.Uri.AbsoluteUri;
            }
            Picture uploadedPicture = new Picture();
            uploadedPicture.Path = uplPath;
            uploadedPicture.Name = uploadResult.PublicId;
            pictureRepository.Insert(uploadedPicture);
            return Json(new { path = uplPath, });
        }

        public ActionResult TagAutocompliteSearch(string term)
        {
            if (term == "" || term == null)
            {
                List<String> tags = tagRepository.Get().Select(tag => tag.Text).Distinct().ToList();
                return Json(tags, JsonRequestBehavior.AllowGet);
            }
            List<String> models = tagRepository.Get().Where(tag => tag.Text.Contains(term)).Select(tag => tag.Text).Distinct().ToList();
            return Json(models, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCategoties()
        {
            var categories = categoryRepository.Get().Select(category => new { value = category.Text });
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

    }
}