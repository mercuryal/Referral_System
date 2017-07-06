using System.Web;
using System.Web.Optimization;

namespace MvcApplication1
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Dashboard bundles
            bundles.Add(new StyleBundle("~/Content/themes/mytheme/css").Include(
                    "~/Content/bootstrap.css",
                    "~/Content/themes/mytheme/css/bootstrap-responsive.css",
                    "~/Content/themes/mytheme/css/colorpicker.css",
                    "~/Content/themes/mytheme/css/datepicker.css",
                    "~/Content/themes/mytheme/css/uniform.css",
                    "~/Content/themes/mytheme/css/select2.css",
                    "~/Content/themes/mytheme/css/fullcalendar.css",
                    "~/Content/themes/mytheme/css/matrix-style.css",
                    "~/Content/themes/mytheme/css/matrix-media.css",
                    "~/Content/themes/mytheme/font-awesome/css/font-awesome.css",
                    "~/Content/themes/mytheme/css/jquery.gritter.css",
                    "~/Content/themes/mytheme/css/bootstrap-wysihtml5.css"
                ));
            bundles.Add(new StyleBundle("~/Content/themes/userTheme/css").Include(
                    "~/Content/themes/userTheme/assets/css/bootstrap.css",
                    "~/Content/themes/userTheme/assets/css/animate.css",
                    "~/Content/themes/userTheme/assets/css/demo.css",
                    "~/Content/themes/userTheme/assets/css/light-bootstrap-dashboard.css",
                    "~/Content/themes/userTheme/assets/css/pe-icon-7-stroke.css"
                ));

            bundles.Add(new StyleBundle("~/Content/themes/mytheme/css/account").Include(
                    "~/Content/themes/mytheme/css/bootstrap.css",
                "~/Content/themes/mytheme/css/bootstrap-responsive.css",
                "~/Content/themes/mytheme/css/matrix-login.css",
                "~/Content/themes/mytheme/font-awesome/css/font-awesome.css"
                ));

            bundles.Add(new ScriptBundle("~/Content/themes/mytheme/js").Include(
                        "~/Content/themes/mytheme/js/jquery.js",
                        "~/Content/themes/mytheme/js/excanvas.js",
                        "~/Content/themes/mytheme/js/jquery.ui.custom.js",
                        "~/Content/themes/mytheme/js/bootstrap.js",
                        "~/Content/themes/mytheme/js/jquery.flot.js",
                        "~/Content/themes/mytheme/js/jquery.flot.resize.js",
                        "~/Content/themes/mytheme/js/jquery.peity.js",
                        "~/Content/themes/mytheme/js/fullcalendar.js",
                        "~/Content/themes/mytheme/js/matrix.js",
                        "~/Content/themes/mytheme/js/matrix.dashboard.js",
                        "~/Content/themes/mytheme/js/jquery.gritter.js",
                        "~/Content/themes/mytheme/js/matrix.interface.js",
                        "~/Content/themes/mytheme/js/matrix.chat.js",
                        "~/Content/themes/mytheme/js/jquery.validate.js",
                        "~/Content/themes/mytheme/js/matrix.form_validation.js",
                        "~/Content/themes/mytheme/js/jquery.wizard.js",
                        "~/Content/themes/mytheme/js/jquery.uniform.js",
                        "~/Content/themes/mytheme/js/select2.js",
                        "~/Content/themes/mytheme/js/matrix.popover.js",
                        "~/Content/themes/mytheme/js/jquery.dataTables.js",
                        "~/Content/themes/mytheme/js/matrix.tables.js"
                        ));

            bundles.Add(new ScriptBundle("~/Content/userTheme/assets/js").Include(
                        "~/Content/themes/userThemes/assets/js/jquery-1.10.2.js",
                        "~/Content/themes/userThemes/assets/js/bootstrap-checkbox-radio-switch.js",
                        "~/Content/themes/userThemes/assets/js/bootstrap-notify.js",
                        "~/Content/themes/userThemes/assets/js/bootstrap-select",
                        "~/Content/themes/userThemes/assets/js/bootstrap.js",
                        "~/Content/themes/userThemes/assets/js/chartist.js",
                        "~/Content/themes/userThemes/assets/js/demo.js",
                        "~/Content/themes/userThemes/assets/js/light-bootstrap-dashboard.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/Content/themes/mytheme/loginjs").Include(
                        "~/Content/themes/mytheme/js/jquery.js",
                        "~/Content/themes/mytheme/js/matrix.login.js"
                        ));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"
                ));
        }
    }
}