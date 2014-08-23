using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using CloudinaryDotNet.Actions;
using CourseProject.Models;
using CourseProject.Repository;
using CourseProject.Repository.Interfaces;
using CourseProject.ViewModels;
using CourseProject.View_Models;
using CloudinaryDotNet;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MultilingualSite.Filters;

namespace CourseProject.Controllers
{
    [Culture]
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

        Dictionary<int, String> categoriesIdToString = new Dictionary<int, string>();

        Dictionary<String, int> categoriesStringToId = new Dictionary<String, int>();

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

        private void InitCategoriesIdToString()
        {
            categoriesIdToString.Clear();
            categoriesIdToString.Add(1, Resources.Resource.CategoryCulture);
            categoriesIdToString.Add(2, Resources.Resource.CategoryMath);
            categoriesIdToString.Add(3, Resources.Resource.CategoryArt);
            categoriesIdToString.Add(4, Resources.Resource.CategoryPhysics);
            categoriesIdToString.Add(5, Resources.Resource.CategoryPeople);
            categoriesIdToString.Add(6, Resources.Resource.CategoryWorld);
            categoriesIdToString.Add(7, Resources.Resource.CategoryScience);
        }

        private void InitCategoriesStringToId()
        {
            categoriesStringToId.Clear();
            categoriesStringToId.Add(Resources.Resource.CategoryCulture, 1);
            categoriesStringToId.Add(Resources.Resource.CategoryMath, 2);
            categoriesStringToId.Add(Resources.Resource.CategoryArt, 3);
            categoriesStringToId.Add(Resources.Resource.CategoryPhysics, 4);
            categoriesStringToId.Add(Resources.Resource.CategoryPeople, 5);
            categoriesStringToId.Add(Resources.Resource.CategoryWorld, 6);
            categoriesStringToId.Add(Resources.Resource.CategoryScience, 7);
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateExercise()
        {
            ViewBag.categories = categoryRepository.Get().Select(category => category.Text);
            return View();
        }

        [Authorize]
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
            InitCategoriesIdToString();
            InitCategoriesStringToId();
            Exercise exercise = new Exercise();
            exercise.Active = true;
            exercise.Author = userManager.FindById(User.Identity.GetUserId());
            int id = categoriesStringToId[model.Category];
            Category categ = categoryRepository.GetByID(id);
            exercise.Category = categ;
            exercise.Name = model.Name;
            exercise.Text = model.Text;
            exercise.TriesOfAnswers = 0;
            exerciseRepository.Insert(exercise);
            exercise.Answers = GetAnswersForCreateExercise(model.Answers, exercise);
            exercise.Graphs = Graphs(model.Graphs, exercise);
            exercise.Pictures = Pictures(model.Pictures, exercise);
            exercise.Tags = AddingTags(model.Tags);
            exercise.Equations = Equations(model.Formulas, exercise);
            exercise.Videos = Videos(model.Videos, exercise);
            return exercise;
        }

#region Create Exercise Helpers

        private ICollection<Video> Videos(string encodedVideos, Exercise exercise)
        {
            ICollection<Video> videos = new Collection<Video>();
            if (!String.IsNullOrEmpty(encodedVideos))
            {
                List<String> modelVideos = System.Web.Helpers.Json.Decode<List<String>>(encodedVideos);
                foreach (string modelVideo in modelVideos)
                {
                    Video video = new Video();
                    video.Path = modelVideo;
                    video.Task = exercise;
                    videos.Add(video);
                    videoRepository.Insert(video);
                }
            }
            return videos;
        }

        private ICollection<Equation> Equations(string encodedFormulas, Exercise exercise)
        {
            ICollection<Equation> equations = new Collection<Equation>();
            if (!String.IsNullOrEmpty(encodedFormulas))
            {
                List<String> modelFormulas = System.Web.Helpers.Json.Decode<List<String>>(encodedFormulas);
                foreach (String eq in modelFormulas)
                {
                    Equation equation = new Equation();
                    equation.Path = eq;
                    equation.Task = exercise;
                    equations.Add(equation);
                    equationRepository.Insert(equation);
                }
            }
            return equations;
        }

        private ICollection<Tag> AddingTags(string encodedTags)
        {
            List<String> modelTags = System.Web.Helpers.Json.Decode<List<String>>(encodedTags);
            ICollection<Tag> addingTags = new Collection<Tag>();
            if (!String.IsNullOrEmpty(encodedTags))
            {
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
            }
            return addingTags;
        }

        private ICollection<Picture> Pictures(string encodedPictures, Exercise exercise)
        {
            ICollection<Picture> pictures = new Collection<Picture>();
            if (!String.IsNullOrEmpty(encodedPictures))
            {
                List<String> pictureSources = System.Web.Helpers.Json.Decode<List<String>>(encodedPictures);
                foreach (String pictureSource in pictureSources)
                {
                    Picture picture = new Picture();
                    picture.Path = pictureSource;
                    picture.Task = exercise;
                    pictures.Add(picture);
                    pictureRepository.Insert(picture);
                }
            }

            return pictures;
        }

        private ICollection<Graph> Graphs(String encodedGraphs, Exercise exercise)
        {
            ICollection<Graph> graphs = new Collection<Graph>();
            if (!String.IsNullOrEmpty(encodedGraphs))
            {
                List<String> modelGraphs = System.Web.Helpers.Json.Decode<List<String>>(encodedGraphs);
                foreach (String info in modelGraphs)
                {
                    Graph graph = new Graph();
                    graph.Path = info;
                    graph.Task = exercise;
                    graphs.Add(graph);
                    graphRepository.Insert(graph);
                }
            }
            return graphs;
        }

        private ICollection<Answer> GetAnswersForCreateExercise(String encodedAnswers, Exercise exercise)
        {
            ICollection<Answer> answers = new Collection<Answer>();
            if (!String.IsNullOrEmpty(encodedAnswers))
            {
                List<String> modelAnswers = System.Web.Helpers.Json.Decode<List<String>>(encodedAnswers);
                foreach (String ans in modelAnswers)
                {
                    Answer answer = new Answer();
                    answer.Text = ans;
                    answer.Task = exercise;
                    answers.Add(answer);
                    answerRepository.Insert(answer);
                }
            }
            return answers;
        }

#endregion

        [Authorize]
        [HttpPost]
        public ActionResult AddEvaluation(int id, string evaluationButton)
        {
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            Exercise exercise = exerciseRepository.GetByID(id);
            if (exercise.Author.Id != user.Id && Request.IsAuthenticated && exercise.Active)
            {
                Evaluation previousEvaluation = evaluationRepository.Get().FirstOrDefault(localEvaluation => localEvaluation.Target.Id == id && localEvaluation.User == user);
                if (previousEvaluation != null)
                {
                    bool type = previousEvaluation.Type;
                    switch (evaluationButton)
                    {
                        case "like":
                            if (!type)
                            {
                                previousEvaluation.Type = true;
                            }
                            break;
                        case "dislike":
                            if (type)
                            {
                                previousEvaluation.Type = false;
                            }
                            break;
                        default:
                            return RedirectToAction("ShowExercise", new { id = id });
                    }
                    evaluationRepository.Update(previousEvaluation);
                    return RedirectToAction("ShowExercise", new { id = id });
                }
                else
                {
                    Evaluation newEvaluation = new Evaluation();
                    newEvaluation.Target = exercise;
                    newEvaluation.User = user;
                    switch (evaluationButton)
                    {
                        case "like":
                            newEvaluation.Type = true;
                            break;
                        case "dislike":
                            newEvaluation.Type = false;
                            break;
                        default:
                            return RedirectToAction("ShowExercise", new { id = id });
                    }
                    evaluationRepository.Insert(newEvaluation);
                    return RedirectToAction("ShowExercise", new { id = id });
                }
            }
            return RedirectToAction("ShowExercise", new { id = id });
        }

        [Authorize]
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

        [Authorize]
        [HttpPost]
        public ActionResult AddComment(AddCommentViewModel model)
        {
            ApplicationUser user = userManager.FindById(model.UserId);
            Exercise exercise = exerciseRepository.GetByID(model.ExerciseId);
            Comment newComment = new Comment();
            newComment.Target = exercise;
            newComment.Text = model.Text;
            newComment.Author = user;
            newComment.Target = exercise;
            newComment.AuthorId = user.Id;
            commentRepository.Insert(newComment);
            exercise.Comments.Add(newComment);
            exerciseRepository.Update(exercise);
            return RedirectToAction("ShowExercise", "Exercise", new { id = model.ExerciseId });
        }

        [Authorize]
        [HttpPost]
        public ActionResult SendAnswer(SendAnswerPartialViewModel model)
        {
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            Exercise exercise = exerciseRepository.GetByID(model.TaskId);
            List<string> answers = exerciseRepository.GetByID(model.TaskId).Answers.Select(localAnswer => localAnswer.Text).ToList();
            bool answerFound = false;
            for (int i = 0; i < answers.Count() && !answerFound; i++)
            {
                if (answers[i] == model.Answer)
                {
                    answerFound = true;
                    exercise.RightAnsweredUsers.Add(user);
                    exercise.TriesOfAnswers = exercise.TriesOfAnswers + 1;
                    exerciseRepository.Update(exercise);
                    user.RightAnswers.Add(exercise);
                    userManager.Update(user);
                    TempData["alertMessage"] = "You answered right!";
                }
            }
            if (!answerFound)
            {
                exercise.TriesOfAnswers = exercise.TriesOfAnswers + 1;
                exerciseRepository.Update(exercise);
            }
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }

        [HttpGet]
        public ActionResult ShowExercise(int id)
        {
            InitCategoriesIdToString();
            Exercise exercise = exerciseRepository.GetByID(id);
            ViewBag.IsRightAnweredUser = true;
            if (exercise.RightAnsweredUsers.Contains(userManager.FindById(User.Identity.GetUserId())))
            {
                ViewBag.IsRightAnweredUser = true;
            }
            exercise.Category.Text = categoriesIdToString[exercise.Category.Id]; 
            return View(exercise);
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditExercise(int id)
        {
            InitCategoriesIdToString();
            Exercise exercise = exerciseRepository.GetByID(id);
            EditExerciseViewModel model = new EditExerciseViewModel();
            model.Exercise = exercise;
            model.Exercise.Category.Text = categoriesIdToString[model.Exercise.Category.Id]; 
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditExercise(EditExerciseViewModel model)
        {
            Exercise exercise = exerciseRepository.GetByID(model.Exercise.Id);
            exercise.Name = model.Name;
            exercise.Text = model.Text;
            SetNewAnswers(ref exercise, model.Answers);
            SetNewTags(ref exercise, model.Tags);
            SetNewEquations(model.Equations, ref exercise);
            SetNewGraphs(model.Graphs, ref exercise);
            SetNewVideos(model.Videos, ref exercise);
            SetNewPictures(model.Pictures, ref exercise);
            exerciseRepository.Update(exercise);
            return RedirectToAction("Index", "Home");
        }

        #region Edit Exercise Helpers

        private void SetNewTags(ref Exercise exercise, string encodedTags)
        {
            List<String> oldTags = exercise.Tags.Select(tag => tag.Text).ToList();
            List<String> newTags = System.Web.Helpers.Json.Decode<List<String>>(encodedTags);
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
        }

        private void SetNewAnswers(ref Exercise exercise, string encodedAnswers)
        {
            List<String> oldAnswers = exercise.Answers.Select(answer => answer.Text).ToList();
            List<String> newAnswers = System.Web.Helpers.Json.Decode<List<String>>(encodedAnswers);
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
        }

        private void SetNewPictures(string encodedPictures, ref Exercise exercise)
        {
            if (!String.IsNullOrEmpty(encodedPictures))
            {
                List<String> oldPictures = exercise.Pictures.Select(p => p.Path).ToList();
                List<String> newPictures = System.Web.Helpers.Json.Decode<List<String>>(encodedPictures);
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
        }

        private void SetNewVideos(string encodedVideos, ref Exercise exercise)
        {
            if (!String.IsNullOrEmpty(encodedVideos))
            {
                ICollection<Video> videos = new Collection<Video>();
                List<String> oldVideos = exercise.Videos.Select(v => v.Path).ToList();
                List<String> newVideos = System.Web.Helpers.Json.Decode<List<String>>(encodedVideos);

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
        }

        private void SetNewGraphs(string encodedGraphs, ref Exercise exercise)
        {
            if (!String.IsNullOrEmpty(encodedGraphs))
            {
                ICollection<Graph> graphs = new Collection<Graph>();
                List<String> oldGraphs = exercise.Graphs.Select(graph => graph.Path).ToList();
                List<String> newGraphs = System.Web.Helpers.Json.Decode<List<String>>(encodedGraphs);

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
        }

        private void SetNewEquations(string encodedEquations, ref Exercise exercise)
        {
            if (!String.IsNullOrEmpty(encodedEquations))
            {
                ICollection<Equation> equations = new Collection<Equation>();
                List<String> oldEquations = exercise.Equations.Select(equation => equation.Path).ToList();
                List<String> newEquations = System.Web.Helpers.Json.Decode<List<String>>(encodedEquations);

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
            else
            {
                ICollection<Equation> equations = exercise.Equations;
                foreach (var equation in equations)
                {
                    exercise.Equations.Remove(equation);
                    graphRepository.Delete(equation);
                }
            }
        }

        #endregion
        
        [Authorize]
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

        [Authorize]
        [HttpGet]
        public ActionResult AddAnswer()
        {
            return RedirectToAction("MyProfile", "Profile");
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddAnswer(SendAnswerPartialViewModel model)
        {
            Exercise exercise = exerciseRepository.GetByID(model.TaskId);
            Answer newAnswer = new Answer();
            newAnswer.Task = exercise;
            newAnswer.Text = model.Answer;
            answerRepository.Update(newAnswer);
            exerciseRepository.Update(exercise);
            return RedirectToAction("MyProfile", "Profile");
        }

        [Authorize]
        [HttpPost]
        public ActionResult WriteToAuthor(int id, string text)
        {
            Exercise exercise = exerciseRepository.GetByID(id);
            //TODO: email
            return RedirectToAction("MyProfile", "Profile");
        }

        [Authorize]
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
                uplPath = uplPath.Insert(insertIndex, setImageSize);
            }
            else
            {
                uplPath = uploadResult.Uri.AbsoluteUri;
            }
            Picture uploadedPicture = new Picture();
            uploadedPicture.Path = uplPath;
            uploadedPicture.Name = uploadResult.PublicId;
            pictureRepository.Insert(uploadedPicture);
            return Json(new { path = uplPath });
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
            InitCategoriesIdToString();
            InitCategoriesStringToId();
            var categories = categoryRepository.Get();
            List<String> categoriesText = new List<string>();
            foreach (var category in categories)
            {
                int id = category.Id;
                categoriesText.Add(categoriesIdToString[id]);
            }
            return Json(categoriesText, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetComments(int id)
        {
            IEnumerable<GetCommentViewModel> model = getCommentViewModels(1, 5, id);
            return PartialView(model);
        }

        private List<GetCommentViewModel> getCommentViewModels(int BlockNumber, int BlockSize, int id)
        {
            int startIndex = (BlockNumber - 1) * BlockSize;
            var allComments = commentRepository.Get().Where(comment => comment.Target.Id == id).OrderByDescending(comment => comment.Id);
            var comments = allComments.Skip(startIndex).Take(BlockSize).ToList();
            List<GetCommentViewModel> model = new List<GetCommentViewModel>();
            foreach (var comment in comments)
            {
                if (comment != null)
                {
                    GetCommentViewModel element = new GetCommentViewModel();
                    element.AuthorId = comment.AuthorId;
                    ApplicationUser author = UserManager.FindById(comment.AuthorId);
                    element.AuthorAvatar = author.ImagePath;
                    element.AuthorName = author.UserName;
                    element.Text = comment.Text;
                    element.Id = comment.Id;
                    model.Add(element);
                }
            }
            return model;
        }

        [HttpPost]
        public ActionResult InfinateScroll(int blockNumber, int id)
        {
            string html;
            bool noMoreData = false;
            const int BlockSize = 5;
            var comments = getCommentViewModels(blockNumber, BlockSize, id);
            if (comments.Count() == 0)
            {
                noMoreData = true;
                html = "";
            }
            else
            {
                html = RenderPartialViewToString("GetComments", comments);
            }

            return Json(new { NoMoreData = noMoreData, HTMLString = html });
        }

        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        [HttpPost]
        public ActionResult Search(string search)
        {
            var searchReturn = new SearchReturnModel();
            LuceneSearch luceneSearch = new LuceneSearch(exerciseRepository);
            searchReturn.Exercises = luceneSearch.SearchExercise(search).Distinct();
            LuceneSearchUserName luceneSearchUserName = new LuceneSearchUserName(userManager);
            searchReturn.Users = luceneSearchUserName.SearchUser(search).Distinct();

            return View("SearchResults", searchReturn);//"Rating", MvcApplication.dataBase.UserRepository.Get().OrderBy(user => user.RightAnswers.Count()));
        }

        public ActionResult SendAnswerPartialView(int id)
        {
            SendAnswerPartialViewModel model = new SendAnswerPartialViewModel();
            model.TaskId = id;
            return PartialView("_SendAnswer", model);
        }

        public ActionResult DeleteComment(int id)
        {
            commentRepository.Delete(id);
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
    }
}