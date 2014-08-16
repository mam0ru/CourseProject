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

            bundles.Add(new ScriptBundle("~/bundles/exercise/create/fileuploadsetting").Include("~/Scripts/Exercise/Create/fileupload.js"));

            bundles.Add(new ScriptBundle("~/bundles/exercise/create/formulas").Include("~/Scripts/Exercise/Create/addFormulas.js"));

            bundles.Add(new ScriptBundle("~/bundles/exercise/create/autocomplite").Include("~/Scripts/Exercise/Create/aoutocomplite.js"));

            bundles.Add(new ScriptBundle("~/bundles/exercise/create/fillcategories").Include(
                "~/Scripts/Exercise/Create/fillCategories.js"));

            bundles.Add(new ScriptBundle("~/bundles/exercise/create/addAnswer").Include(
                "~/Scripts/Exercise/Create/addAnswer.js"));

            bundles.Add(new ScriptBundle("~/bundles/exercise/taginput").Include(
                "~/Scripts/Exercise/tags/js/textext.core.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.ajax.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.autocomplete.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.tags.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.prompt.js",
                "~/Scripts/Exercise/Create/TagAndAnswerInputSetting.js"));

            bundles.Add(new ScriptBundle("~/bundles/exercise/edit/taginput").Include(
                "~/Scripts/Exercise/tags/js/textext.core.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.ajax.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.autocomplete.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.tags.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.prompt.js",
                "~/Scripts/Exercise/Edit/TagAndAnswerInputSetting.js"));

            bundles.Add(new StyleBundle("~/Content/taginput").Include(
                "~/Content/tags/css/textext.core.css",
                "~/Content/tags/css/textext.plugin.tags.css",
                "~/Content/tags/css/textext.plugin.prompt.css",
                "~/Content/tags/css/textext.plugin.autocomplete.css"));

            bundles.Add(new ScriptBundle("~/bundles/EquationEditor").Include(
                "~/Scripts/Exercise/EquationEditor/eq_config.js",
                "~/Scripts/Exercise/EquationEditor/eq_editor-lite-17.js"));

            bundles.Add(new StyleBundle("~/Content/equationEditor").Include(
                "~/Content/EquationEditor/equation-embed.css"));

            bundles.Add(new ScriptBundle("~/byndles/Graphs").Include("~/Scripts/Exercise/Graph/parser.js",
                "~/Scripts/Exercise/Graph/jqplot/jquery.jqplot.js",
                "~/Scripts/Exercise/Graph/jqplot/plugins/jqplot.canvasTextRenderer.min.js",
                "~/Scripts/Exercise/Graph/jqplot/plugins/jqplot.canvasAxisLabelRenderer.min.js"));

            bundles.Add(new StyleBundle("~/Content/Graphs").Include("~/Сontent/Graph/jquery.jqplot.css"));
            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
