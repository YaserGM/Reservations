using System.Web;
using System.Web.Optimization;

namespace Reservations.App
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


            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/bootstrap.css", "~/Content/site.css",
                    "~/Content/Index.css", "~/Content/create_edit.css",
                    "~/Content/quill/quill.snow.css", "~/Content/quill/quill.bubble.css",
                    "~/Content/star-rating.css"));

            bundles.Add(new ScriptBundle("~/bundles/modules_js")
                .Include("~/Scripts/quill/quill.min.js",
                "~/Scripts/knockout-latest.js", "~/Scripts/knockout-latest.debug.js",
                    "~/Scripts/star-rating.js"));
        }
    }
}