using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CourseProject.Models;
using CourseProject.Repository;
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
        public static IKernel appKernel;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            appKernel = new StandardKernel(new ExerciseNinjectModule());
            Repository.Repository.ProjectDAO DataBase = new Repository.Repository.ProjectDAO();
            ExerciseBuildIndex(DataBase.GetExercises());
            CommentsBuildIndex(DataBase.GetComments());
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
            indexWriter.Close();
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
            indexWriter.Close();
        }
    }

    public class ExerciseNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<Repository.Repository.IRepositoriable>().To<Repository.Repository.ProjectDAO>();
        }
    }
}
