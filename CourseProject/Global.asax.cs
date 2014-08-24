using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CourseProject.Controllers;
using CourseProject.Models;
using CourseProject.Repository.Implementation;
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
            var context = new ApplicationDbContext();
            //TODO: uncomment for initialize database: admin and role
            //Database.SetInitializer<ApplicationDbContext>(new AppDbInitializer(new ApplicationUserRepository(context), new CategoryRepository(context)));               
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
