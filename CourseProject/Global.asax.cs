using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CourseProject.Models;
using CourseProject.Repository;
using CourseProject.Repository.Implementation;
using CourseProject.Repository.Interfaces;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Ninject;
using Ninject.Modules;

namespace CourseProject
{
    public class MvcApplication : System.Web.HttpApplication
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

        public MvcApplication()
        {
            
        }
        public MvcApplication(IExerciseRepository exerciseRepository,
            ICategoryRepository categoryRepository,
            IPictureRepository pictureRepository,
            IAnswerRepository answerRepository,
            ICommentRepository commentRepository,
            ITagRepository tagRepository,
            IEvaluationRepository evaluationRepository,
            IEquationRepository equationRepository,
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
            this.equationRepository = equationRepository;
            this.graphRepository = graphRepository;
            this.videoRepository = videoRepository;
        }
        protected void Application_Start()
        {
           //TODO: initializer must work!!!!!!IMPORTANT 
           Database.SetInitializer<ApplicationDbContext>(new AppDbInitializer(new ApplicationUserRepository(new ApplicationDbContext())));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //ExerciseBuildIndex(exerciseRepository.Get());
            //CommentsBuildIndex(commentRepository.Get());
        }

        public void ExerciseBuildIndex(IEnumerable<Exercise> exercises)
        {
            FSDirectory directory = FSDirectory.Open(new System.IO.DirectoryInfo("C:\\temp\\"));

            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);

            IndexWriter indexWriter = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED);

            foreach (Exercise exercise in exercises)
            {
                indexWriter.AddDocument(exercise.GetDocument());
            }

            indexWriter.Optimize();
            indexWriter.Dispose();
        }

        public void CommentsBuildIndex(IEnumerable<Comment> comments)
        {
            FSDirectory directory = FSDirectory.Open(new System.IO.DirectoryInfo("C:\\temp\\"));

            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);

            IndexWriter indexWriter = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED);

            foreach (Comment comment in comments)
            {
                indexWriter.AddDocument(comment.GetDocument());
            }

            indexWriter.Optimize();
            indexWriter.Dispose();
        }
    }
}
