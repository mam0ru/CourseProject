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
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/exercise/create/markdown").Include(
                        "~/Scripts/markdown/js/markdown.js",
                        "~/Scripts/markdown/js/to-markdown.js",
                        "~/Scripts/markdown/js/bootstrap-markdown.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/Exercise/Create/EN").Include(
                "~/Scripts/Latex/eq_config.js",
                "~/Scripts/Latex/eq_editor-lite-17.js",
                "~/Scripts/Latex/latexit.js",
                "~/Scripts/FileUpload/jqueryui/jquery.ui.widget.js",
                "~/Scripts/FileUpload/jquery.fileupload.js",
                "~/Scripts/FileUpload/jquery.fileupload-ui.js",
                "~/Scripts/FileUpload/jquery.iframe-transport.js",
                "~/Scripts/Exercise/Create/fillCategories.js",
                "~/Scripts/Exercise/tags/js/textext.core.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.ajax.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.autocomplete.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.tags.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.prompt.js",
                "~/Scripts/Exercise/Create/TagAndAnswerInputSetting.js",
                "~/Scripts/Exercise/Graph/parser.js",
                "~/Scripts/Exercise/Graph/jqplot/jquery.jqplot.js",
                "~/Scripts/Exercise/Graph/jqplot/plugins/jqplot.canvasTextRenderer.min.js",
                "~/Scripts/Exercise/Graph/jqplot/plugins/jqplot.canvasAxisLabelRenderer.min.js",
                "~/Scripts/Exercise/Create/Graph.js",
                "~/Scripts/Exercise/Create/setting.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/Exercise/Create/RU").Include(
                "~/Scripts/Latex/eq_config.js",
                "~/Scripts/Latex/eq_editor-lite-17.js",
                "~/Scripts/Latex/latexit.js",
                "~/Scripts/FileUpload/jqueryui/jquery.ui.widget.js",
                "~/Scripts/FileUpload/jquery.fileupload.js",
                "~/Scripts/FileUpload/jquery.fileupload-ui.js",
                "~/Scripts/FileUpload/jquery.iframe-transport.js",
                "~/Scripts/Exercise/Create/RU/fillCategories.js",
                "~/Scripts/Exercise/tags/js/textext.core.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.ajax.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.autocomplete.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.tags.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.prompt.js",
                "~/Scripts/Exercise/Create/RU/TagAndAnswerInputSetting.js",
                "~/Scripts/Exercise/Graph/parser.js",
                "~/Scripts/Exercise/Graph/jqplot/jquery.jqplot.js",
                "~/Scripts/Exercise/Graph/jqplot/plugins/jqplot.canvasTextRenderer.min.js",
                "~/Scripts/Exercise/Graph/jqplot/plugins/jqplot.canvasAxisLabelRenderer.min.js",
                "~/Scripts/Exercise/Create/RU/Graph.js",
                "~/Scripts/Exercise/Create/RU/setting.js"));
            bundles.Add(new ScriptBundle("~/bundles/Exercise/Edit/EN").Include(
                "~/Scripts/Latex/eq_config.js",
                "~/Scripts/Latex/eq_editor-lite-17.js",
                "~/Scripts/Latex/latexit.js",
                "~/Scripts/Exercise/Edit/DisplayGraphs.js",
                "~/Scripts/FileUpload/jqueryui/jquery.ui.widget.js",
                "~/Scripts/FileUpload/jquery.fileupload.js",
                "~/Scripts/FileUpload/jquery.fileupload-ui.js",
                "~/Scripts/FileUpload/jquery.iframe-transport.js",
                "~/Scripts/Exercise/Create/fillCategories.js",
                "~/Scripts/Exercise/Edit/setting.js",
                "~/Scripts/Exercise/Graph/parser.js",
                "~/Scripts/Exercise/Graph/jqplot/jquery.jqplot.js",
                "~/Scripts/Exercise/Graph/jqplot/plugins/jqplot.canvasTextRenderer.min.js",
                "~/Scripts/Exercise/Graph/jqplot/plugins/jqplot.canvasAxisLabelRenderer.min.js",
                "~/Scripts/Exercise/Edit/Graph.js",
                "~/Scripts/Exercise/tags/js/textext.core.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.ajax.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.autocomplete.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.tags.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.prompt.js",
                "~/Scripts/Exercise/Edit/TagAndAnswerInputSetting.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/Exercise/Edit/EN").Include(
                "~/Scripts/Latex/eq_config.js",
                "~/Scripts/Latex/eq_editor-lite-17.js",
                "~/Scripts/Latex/latexit.js",
                "~/Scripts/Exercise/Edit/DisplayGraphs.js",
                "~/Scripts/FileUpload/jqueryui/jquery.ui.widget.js",
                "~/Scripts/FileUpload/jquery.fileupload.js",
                "~/Scripts/FileUpload/jquery.fileupload-ui.js",
                "~/Scripts/FileUpload/jquery.iframe-transport.js",
                "~/Scripts/Exercise/Create/fillCategories.js",
                "~/Scripts/Exercise/Edit/setting.js",
                "~/Scripts/Exercise/Graph/parser.js",
                "~/Scripts/Exercise/Graph/jqplot/jquery.jqplot.js",
                "~/Scripts/Exercise/Graph/jqplot/plugins/jqplot.canvasTextRenderer.min.js",
                "~/Scripts/Exercise/Graph/jqplot/plugins/jqplot.canvasAxisLabelRenderer.min.js",
                "~/Scripts/Exercise/Edit/Graph.js",
                "~/Scripts/Exercise/tags/js/textext.core.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.ajax.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.autocomplete.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.tags.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.prompt.js",
                "~/Scripts/Exercise/Edit/TagAndAnswerInputSetting.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/Exercise/Edit/RU").Include(
                "~/Scripts/Latex/eq_config.js",
                "~/Scripts/Latex/eq_editor-lite-17.js",
                "~/Scripts/Latex/latexit.js",
                "~/Scripts/Exercise/Edit/RU/DisplayGraphs.js",
                "~/Scripts/FileUpload/jqueryui/jquery.ui.widget.js",
                "~/Scripts/FileUpload/jquery.fileupload.js",
                "~/Scripts/FileUpload/jquery.fileupload-ui.js",
                "~/Scripts/FileUpload/jquery.iframe-transport.js",
                "~/Scripts/Exercise/Create/fillCategories.js",
                "~/Scripts/Exercise/Edit/RU/setting.js",
                "~/Scripts/Exercise/Graph/parser.js",
                "~/Scripts/Exercise/Graph/jqplot/jquery.jqplot.js",
                "~/Scripts/Exercise/Graph/jqplot/plugins/jqplot.canvasTextRenderer.min.js",
                "~/Scripts/Exercise/Graph/jqplot/plugins/jqplot.canvasAxisLabelRenderer.min.js",
                "~/Scripts/Exercise/Edit/RU/Graph.js",
                "~/Scripts/Exercise/tags/js/textext.core.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.ajax.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.autocomplete.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.tags.js",
                "~/Scripts/Exercise/tags/js/textext.plugin.prompt.js",
                "~/Scripts/Exercise/Edit/RU/TagAndAnswerInputSetting.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/Exercise/Show/EN").Include(
                "~/Scripts/Latex/latexit.js",
                "~/Scripts/Exercise/Graph/parser.js",
                "~/Scripts/Exercise/Graph/jqplot/jquery.jqplot.js",
                "~/Scripts/Exercise/Graph/jqplot/plugins/jqplot.canvasTextRenderer.min.js",
                "~/Scripts/Exercise/Graph/jqplot/plugins/jqplot.canvasAxisLabelRenderer.min.js",
                "~/Scripts/Exercise/Show/DisplayGraphs.js",
                "~/Scripts/Exercise/Show/SendEmail.js",
                "~/Scripts/Exercise/Show/InfiniteScrolling.js",
                "~/Scripts/jquery.unobtrusive-ajax.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/Exercise/Show/RU").Include(
                "~/Scripts/Latex/latexit.js",
                "~/Scripts/Exercise/Graph/parser.js",
                "~/Scripts/Exercise/Graph/jqplot/jquery.jqplot.js",
                "~/Scripts/Exercise/Graph/jqplot/plugins/jqplot.canvasTextRenderer.min.js",
                "~/Scripts/Exercise/Graph/jqplot/plugins/jqplot.canvasAxisLabelRenderer.min.js",
                "~/Scripts/Exercise/Show/RU/DisplayGraphs.js",
                "~/Scripts/Exercise/Show/RU/SendEmail.js",
                "~/Scripts/Exercise/Show/RU/InfiniteScrolling.js",
                "~/Scripts/jquery.unobtrusive-ajax.js"
                ));
            bundles.Add(new StyleBundle("~/Content/taginput").Include(
                "~/Content/tags/css/textext.core.css",
                "~/Content/tags/css/textext.plugin.tags.css",
                "~/Content/tags/css/textext.plugin.prompt.css",
                "~/Content/tags/css/textext.plugin.autocomplete.css"));
            bundles.Add(new StyleBundle("~/Content/equationEditor").Include(
                "~/Content/EquationEditor/equation-embed.css"));
            bundles.Add(new StyleBundle("~/Content/Graphs").Include("~/Сontent/Graph/jquery.jqplot.css"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/dark").Include("~/Content/bootstrap.css"));
            bundles.Add(new StyleBundle("~/Content/white").Include("~/Content/bootstrap.white.css"));
            bundles.Add(new StyleBundle("~/Content/markdown/css").Include(
                "~/Content/markdown/css/bootstrap-markdown.min.css"));
            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
