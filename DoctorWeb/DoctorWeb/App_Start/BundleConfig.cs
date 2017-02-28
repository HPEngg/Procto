using System.Web;
using System.Web.Optimization;

namespace DoctorWeb
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

            bundles.Add(new ScriptBundle("~/bundles/myscripts").Include(
                "~/Content/js/jquery-ui.min.js",
                "~/Content/js/jquery-1.12.0.min.js",
                "~/Content/autocomplete/jquery-ui.js",
                "~/Content/js/html-table-search.js",
                "~/Content/js/jquery.barrating.js",
                "~/Content/js/examples.js",
                "~/Content/js/webcam.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/css/reset.css",
                      "~/Content/css/font.css",
                      "~/Content/css/picedit.css",
                      "~/Content/css/style.css",
                      "~/Content/css/patient.css",
                      "~/Content/css/responsive-tables.css",
                      "~/Content/css/examples.css",
                      "~/Content/css/fontawesome-stars.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/jquery-ui.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/ourcss").Include(
                     "~/Content/css/reset.css",
                     "~/Content/site.css"));
        }
    }
}
