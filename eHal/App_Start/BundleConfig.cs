using System.Web;
using System.Web.Optimization;

namespace eHal
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



            bundles.Add(new ScriptBundle("~/bundles/semantic").Include(
                      "~/Scripts/semantic.js"));
            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                      "~/Scripts/datepicker.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/rangeSlider").Include(
                      "~/Scripts/ion.rangeSlider.js",
                      "~/Scripts/moment.min.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/semantic.css",
                        "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/datepicker").Include(
                      "~/Content/datepicker.min.css"));

            bundles.Add(new StyleBundle("~/Content/rangeSlider").Include(
                      "~/Content/ion.rangeSlider.css",
                      "~/Content/ion.rangeSlider.skinFlat.css"));


        }
    }
}
