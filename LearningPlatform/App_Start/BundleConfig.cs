using System.Web;
using System.Web.Optimization;

namespace LearningPlatform
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
                    "~/Scripts/jquery.barrating.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                    "~/Scripts/site.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                      "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/plugins").Include(
                    "~/Content/plugins/bars-rating.css"));

            bundles.Add(new StyleBundle("~/Content/styles").Include(
                      "~/Content/*-style.css",
                      "~/Content/util.css",
                      "~/Content/colors.css"));
        }
    }
}
