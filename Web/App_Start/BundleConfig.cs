using System.Web.Optimization;

namespace Sattelite.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //1. STYLES
            bundles.Add(new StyleBundle("~/bundles/main.css")
                        .Include("~/Content/bootstrap.min.css")
                        //.Include("~/Content/bootstrap-social.min.css")
                        //.Include("~/Content/font-awesome.min.css")
                        .Include("~/Content/Site.min.css"));

            bundles.Add(new StyleBundle("~/bundles/admin.css")
                        .Include("~/Content/bootstrap.min.css")
                        //.Include("~/Content/font-awesome.min.css")
                        //.Include("~/Content/bootstrap-social.min.css")
                        .Include("~/Content/admin/adminSite.min.css")
                       );

            bundles.Add(new StyleBundle("~/bundles/login.css")
                        .Include("~/Content/Login.min.css"));

            bundles.Add(new StyleBundle("~/bundles/login.css")
                        .Include("~/Content/login.css"));

            bundles.Add(new StyleBundle("~/bundles/alert.css")
                        .Include("~/Scripts/plugins/sweetalert/dist/sweetalert.css"));


            bundles.Add(new StyleBundle("~/bundles/datatables.css") //filterable DataTable
                        .Include("~/Scripts/plugins/DataTable/datatables.min.css")
                        .Include("~/Scripts/plugins/DataTable/colReorder.bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/bundles/jquery.ui.css").Include(
                        "~/Content/themes/base/jquery.ui.*"
                        //"~/Content/themes/base/jquery.ui.resizable.css",
                        //"~/Content/themes/base/jquery.ui.selectable.css",
                        //"~/Content/themes/base/jquery.ui.accordion.css",
                        //"~/Content/themes/base/jquery.ui.autocomplete.css",
                        //"~/Content/themes/base/jquery.ui.button.css",
                        //"~/Content/themes/base/jquery.ui.dialog.css",
                        //"~/Content/themes/base/jquery.ui.slider.css",
                        //"~/Content/themes/base/jquery.ui.tabs.css",
                        //"~/Content/themes/base/jquery.ui.datepicker.css",
                        //"~/Content/themes/base/jquery.ui.progressbar.css",
                        //"~/Content/themes/base/jquery.ui.theme.css"
                        ));

            //2. SCRIPTS
            bundles.Add(new ScriptBundle("~/bundles/jquery.js").Include(
                        "~/Scripts/jquery.min.js",
                        "~/Scripts/jquery-ui.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery.ui.js").Include(
            //            "~/Scripts/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval.js").Include(
                        //"~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr.js").Include(
                        "~/Scripts/modernizr-*"));


            //"wwwroot/lib/datatables/datatables.js",
            //"wwwroot/lib/datatables/dataTables.colReorder.min.js",

            bundles.Add(new ScriptBundle("~/bundles/bootstrap.js").Include(
                        "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/alert.js").Include(
                        "~/Scripts/plugins/sweetalert/dist/sweetalert.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables.js").Include(
                        "~/Scripts/plugins/DataTable/datatables.min.js",
                        "~/Scripts/plugins/DataTable/datatables.colReorder.min.js"));

        }
    }
}