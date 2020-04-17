using System.Web;
using System.Web.Optimization;

namespace Maladin
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
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            // style for admin
            bundles.Add(new StyleBundle("~/Areas/Admin/allcss").Include(
                "~/Areas/Admin/assets/vendor/font-awesome/css/font-awesome.min.css",
                "~/Areas/Admin/assets/css/fontastic.css",
                "~/Areas/Admin/assets/css/grasp_mobile_progress_circle-1.0.0.min.css",
                "~/Areas/Admin/assets/css/fontastic.css",
                "~/Areas/Admin/assets/css/style.default.css",
                "~/Areas/Admin/assets/css/custom.css",
                "~/Areas/Admin/assets/vendor/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.css"

                ));
            bundles.Add(new ScriptBundle("~/Areas/Admin/alljs").Include(
                "~/Areas/Admin/assets/js/grasp_mobile_progress_circle-1.0.0.min.js",
                "~/Areas/Admin/assets/vendor/jquery.cookie/jquery.cookie.js",
                "~/Areas/Admin/assets/vendor/chart.js/Chart.min.js",
                "~/Areas/Admin/assets/vendor/jquery-validation/jquery.validate.min.js",
                "~/Areas/Admin/assets/vendor/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.concat.min.js",
                "~/Areas/Admin/assets/js/charts-home.js",
                "~/Areas/Admin/assets/js/front.js"
                

                ));
            bundles.Add(new ScriptBundle("~/Areas/Admin/loginjs").Include(
                "~/Areas/Admin/assets/vendor/jquery/jquery.min.js",
                "~/Areas/Admin/assets/popper.js/umd/popper.min.js",
                "~/Areas/Admin/assets/vendor/bootstrap/js/bootstrap.min.js",
                "~/Areas/Admin/assets/js/grasp_mobile_progress_circle-1.0.0.min.js",
                "~/Areas/Admin/assets/vendor/jquery.cookie/jquery.cookie.js",
                "~/Areas/Admin/assets/vendor/chart.js/Chart.min.js",
                "~/Areas/Admin/assets/vendor/jquery-validation/jquery.validate.min.js",
                "~/Areas/Admin/assets/vendor/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.concat.min.js"
                ));
            bundles.Add(new StyleBundle("~/Areas/Partner/allcss").Include(
                "~/Areas/Partner/assets/vendors/mdi/css/materialdesignicons.min.css",
                "~/Areas/Partner/assets/vendors/base/vendor.bundle.base.css",
                "~/Areas/Partner/assets/css/style.css",
                "~/Areas/Partner/assets/vendors/base/vendor.bundle.base.css",
                "~/Areas/Partner/assets/vendors/datatables.net-bs4/dataTables.bootstrap4.css"
                ));
            bundles.Add(new ScriptBundle("~/Areas/Partner/alljs").Include(
                "~/Areas/Partner/assets/vendors/base/vendor.bundle.base.js",
                "~/Areas/Partner/assets/js/off-canvas.js",
                "~/Areas/Partner/assets/js/hoverable-collapse.js",
                "~/Areas/Partner/assets/customJs/home_index.js",
                "~/Areas/Partner/assets/js/template.js",
                "~/Areas/Partner/assets/js/chart.js",
                "~/Areas/Partner/assets/vendors/chart.js/Chart.min.js"
                ));
        }
    }
}
