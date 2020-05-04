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
                      //"~/Scripts/bootstrap.js",
                      "~/assets/js/bootstrap.js",
                      "~/assets/js/bootstrap.min.js",
                      "~/assets/js/popper.min.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/assets/css/bootstrap.min.css",
                      "~/assets/css/bootstrap.css",
                      //"~/Content/bootstrap.min.css",
                      //"~/Content/bootstrap.css",
                      //"~/Content/site.css"
                      "~/Content/main.css"));
            // style for admin
            bundles.Add(new StyleBundle("~/Areas/Admin/allcss").Include(

                "~/Areas/Admin/assets/vendors/mdi/css/materialdesignicons.min.css",
                "~/Areas/Admin/assets/vendors/base/vendor.bundle.base.css",
                "~/Areas/Admin/assets/css/style.css"


                ));

            bundles.Add(new ScriptBundle("~/Areas/Admin/alljs").Include(
  
                "~/Areas/Admin/assets/vendors/base/vendor.bundle.base.js",
                "~/Areas/Admin/assets/vendors/chart.js/Chart.min.js",
                "~/Areas/Admin/assets/vendors/progressbar.js/progressbar.min.js",
                "~/Areas/Admin/assets/vendors/chartjs-plugin-datalabels/chartjs-plugin-datalabels.js",
                "~/Areas/Admin/assets/vendors/justgage/raphael-2.1.4.min.js",
                "~/Areas/Admin/assets/vendors/justgage/justgage.js",
                "~/Areas/Admin/assets/js/dashboard.js",
                "~/Areas/Admin/assets/js/template.js"
                

                ));
            bundles.Add(new ScriptBundle("~/Areas/Admin/loginjs").Include(
                "~/Areas/Admin/assets/vendors/base/vendor.bundle.base.js",
                "~/Areas/Admin/assets/js/template.js"
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
                
                "~/Areas/Partner/assets/vendors/chart.js/Chart.min.js"
                ));
        }
    }
}
