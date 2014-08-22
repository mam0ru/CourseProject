using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CourseProject.Models;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Store;

namespace CourseProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //TODO: uncomment for initialize database: admin and role
            //Database.SetInitializer<ApplicationDbContext>(new AppDbInitializer(new ApplicationUserRepository(new ApplicationDbContext())));               
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
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
