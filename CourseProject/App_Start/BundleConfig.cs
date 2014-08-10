using System.Web;
using System.Web.Optimization;

namespace CourseProject
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/dark").Include("~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/white").Include("~/Content/bootstrap.white.css"));

            bundles.Add(new StyleBundle("~/Content/markdown/css").Include(
                "~/Content/markdown/css/bootstrap-markdown.min.css"));

            //bundles.Add(new ScriptBundle("~/bundles/markdown_bootstrap").Include(
            //            "~/Scripts/markdown/js/bootstrap-markdown.js"));

            //bundles.Add(new ScriptBundle("~/bundles/markdown").Include(
            //            "~/Scripts/markdown/js/markdown.js"));

            //bundles.Add(new ScriptBundle("~/bundles/to_markdown").Include(
            //            "~/Scripts/markdown/js/to-markdown.js"));

            bundles.Add(new ScriptBundle("~/bundles/exercise/create/markdown").Include(
                        "~/Scripts/markdown/js/markdown.js",
                        "~/Scripts/markdown/js/to-markdown.js",
                        "~/Scripts/markdown/js/bootstrap-markdown.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/exercise/create/image").Include(
                        "~/Scripts/FileUpload/jqueryui/jquery.ui.widget.js",
                        "~/Scripts/FileUpload/jquery.fileupload.js",
                        "~/Scripts/FileUpload/jquery.fileupload-ui.js",
                        "~/Scripts/FileUpload/jquery.iframe-transport.js"));

            bundles.Add(new ScriptBundle("~/bundles/exercise/create/formulas").Include("~/Scripts/Exercise/Create/addFormulas.js"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
