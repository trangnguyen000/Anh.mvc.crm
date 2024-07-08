using System.Web;
using System.Web.Optimization;

namespace Anh.mvc.crm
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

            bundles.Add(new ScriptBundle("~/bundles/theme").Include(
                    "~/Theme/plugins/jquery/jquery.min.js",
                    "~/Theme/plugins/jquery-ui/jquery-ui.min.js",
                    "~/Theme/plugins/bootstrap/js/bootstrap.bundle.min.js",
                    "~/Theme/plugins/chart.js/Chart.min.js",
                    "~/Theme/plugins/sparklines/sparkline.js",
                    "~/Theme/plugins/jqvmap/jquery.vmap.min.js",
                    "~/Theme/plugins/jqvmap/maps/jquery.vmap.usa.js",
                    "~/Theme/plugins/jquery-knob/jquery.knob.min.js",
                     "~/Theme/plugins/moment/moment.min.js",
                     "~/Theme/plugins/tempusdominus-bootstrap-4/js/tempusdominus-bootstrap-4.min.js",
                     "~/Theme/plugins/summernote/summernote-bs4.min.js",
                    "~/Theme/plugins/overlayScrollbars/js/jquery.overlayScrollbars.min.js",
                    "~/Theme/dist/js/adminlte.js",
                     "~/Theme/dist/js/demo.js",
                     "~/Theme/plugins/select2/js/select2.full.min.js",
                     "~/Theme/plugins/sweetalert2/sweetalert2.min.js",
                     "~/Theme/plugins/toastr/toastr.min.js",
                    "~/Theme/plugins/daterangepicker/daterangepicker.js",
                     "~/Scripts/vue.min.js",
                     "~/Scripts/base.js"));

            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                  "~/Theme/plugins/jquery/jquery.min.js",
                  "~/Theme/plugins/jquery-ui/jquery-ui.min.js",
                  "~/Theme/plugins/bootstrap/js/bootstrap.bundle.min.js",
                  "~/Theme/dist/js/adminlte.js",
                    "~/Scripts/vue.js",
                   "~/Scripts/vue.min.js",
                   "~/Assets/Js/bLogin.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/theme").Include(
                     "~/Theme/dist/css/front-source-sans.css",
                     "~/Theme/plugins/fontawesome-free/css/all.min.css",
                     "~/Theme/dist/css/ionicons.min.css",
                    "~/Theme/plugins/tempusdominus-bootstrap-4/css/tempusdominus-bootstrap-4.min.css",
                    "~/Theme/plugins/icheck-bootstrap/icheck-bootstrap.min.css",
                    "~/Theme/plugins/jqvmap/jqvmap.min.css",
                    "~/Theme/dist/css/adminlte.min.css",
                    "~/Theme/plugins/overlayScrollbars/css/OverlayScrollbars.min.css",
                    "~/Theme/plugins/daterangepicker/daterangepicker.css",
                     "~/Theme/plugins/summernote/summernote-bs4.min.css",
                     "~/Theme/plugins/daterangepicker/daterangepicker.css",
                     "~/Theme/plugins/select2/css/select2.min.css",
                      "~/Theme/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css",
                      "~/Theme/plugins/toastr/toastr.min.css",
                      "~/Assets/plugins/jqconfirm/jquery-confirm.min.css",
                     "~/Assets/Css/bStyle.css"));

            bundles.Add(new StyleBundle("~/Content/login").Include(
                    "~/Theme/plugins/fontawesome-free/css/all.min.css",
                    "~/Theme/dist/css/ionicons.min.css",
                   "~/Theme/dist/css/adminlte.min.css",
                    "~/Assets/Css/bStyle.css"));
        }
    }
}
