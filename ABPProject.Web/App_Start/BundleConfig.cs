using System.Web.Optimization;

namespace ABPProject.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            //VENDOR RESOURCES

            //~/Bundles/vendor/css
            bundles.Add(
                new StyleBundle("~/Bundles/vendor/css")
                    .Include("~/css/main.css", new CssRewriteUrlTransform())
                    .Include("~/Content/themes/base/all.css", new CssRewriteUrlTransform())
                    .Include("~/Content/bootstrap-paper.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/toastr.min.css", new CssRewriteUrlTransform())
                    .Include("~/Scripts/sweetalert/sweet-alert.css", new CssRewriteUrlTransform())
                    .Include("~/Content/flags/famfamfam-flags.css", new CssRewriteUrlTransform())
                    .Include("~/Content/font-awesome.min.css", new CssRewriteUrlTransform())
                );

            //~/Bundles/vendor/js/top (These scripts should be included in the head of the page)
            bundles.Add(
                new ScriptBundle("~/Bundles/vendor/js/top")
                    .Include(
                        "~/Abp/Framework/scripts/utils/ie10fix.js",
                        "~/Scripts/modernizr-2.8.3.js"
                    )
                );

            //~/Bundles/vendor/bottom (Included in the bottom for fast page load)
            bundles.Add(
                new ScriptBundle("~/Bundles/vendor/js/bottom")
                    .Include(
                        "~/Scripts/json2.min.js",

                        "~/Scripts/jquery-2.2.0.min.js",
                        "~/Scripts/jquery-ui-1.11.4.min.js",

                        "~/Scripts/bootstrap.min.js",

                        "~/Scripts/moment-with-locales.min.js",
                        "~/Content/vendors/jquery-validate/jquery.validate.min.js",
                        "~/Scripts/messages_zh.js",
                        "~/Scripts/jquery.blockUI.js",
                        "~/Scripts/toastr.min.js",
                        "~/Scripts/sweetalert/sweet-alert.min.js",
                        "~/Scripts/others/spinjs/spin.js",
                        "~/Scripts/others/spinjs/jquery.spin.js",

                        "~/Abp/Framework/scripts/abp.js",
                        "~/Abp/Framework/scripts/libs/abp.jquery.js",
                        "~/Abp/Framework/scripts/libs/abp.toastr.js",
                        "~/Abp/Framework/scripts/libs/abp.blockUI.js",
                        "~/Abp/Framework/scripts/libs/abp.spin.js",
                        "~/Abp/Framework/scripts/libs/abp.sweet-alert.js",

                        "~/Scripts/jquery.signalR-2.2.1.min.js"
                    )
                );

            //~/Bundles/css
            bundles.Add(
                new StyleBundle("~/Bundles/css")
                    .Include(
                    //"~/css/main.css",
                    "~/Content/css/css1.css",
                    //"~/Content/css/css2.css",
                    "~/Content/vendors/jquery-ui-1.10.4.custom/css/ui-lightness/jquery-ui-1.10.4.custom.min.css",
                    "~/Content/vendors/font-awesome/css/font-awesome.min.css",
                    "~/Content/vendors/bootstrap/css/bootstrap.min.css",
                    "~/Content/vendors/intro.js/introjs.css",
                    "~/Content/vendors/calendar/zabuto_calendar.min.css",
                    "~/Content/vendors/sco.message/sco.message.css",
                    "~/Content/vendors/intro.js/introjs.css",
                    "~/Content/vendors/animate.css/animate.css",
                    "~/Content/vendors/jquery-pace/pace.css",
                    "~/Content/vendors/iCheck/skins/all.css",
                    "~/Content/vendors/jquery-notific8/jquery.notific8.min.css",
                    "~/Content/vendors/bootstrap-daterangepicker/daterangepicker-bs3.css",
                    "~/css/main-custom.css"
                    )
                );

            //~/Bundles/js
            bundles.Add(
                new ScriptBundle("~/Bundles/js")
                    .Include(
                    "~/js/main.js",
                    //"~/Content/js/jquery-1.10.2.min.js",
                    "~/Content/js/jquery-migrate-1.2.1.min.js",
                    "~/Content/js/jquery-ui.js",
                    "~/Content/vendors/bootstrap-hover-dropdown/bootstrap-hover-dropdown.js",
                    "~/Content/vendors/metisMenu/jquery.metisMenu.js",
                    "~/Content/vendors/slimScroll/jquery.slimscroll.js",
                    "~/Content/vendors/jquery-cookie/jquery.cookie.js",
                    "~/Content/vendors/iCheck/icheck.min.js",
                    "~/Content/vendors/iCheck/custom.min.js",
                    "~/Content/vendors/jquery-notific8/jquery.notific8.min.js",
                    "~/Content/vendors/jquery-highcharts/highcharts.js",
                    "~/Content/js/jquery.menu.js",
                    //"~/Content/vendors/jquery-pace/pace.min.js",
                    "~/Content/vendors/holder/holder.js",
                    "~/Content/vendors/responsive-tabs/responsive-tabs.js",
                    "~/Content/vendors/jquery-news-ticker/jquery.newsTicker.min.js",
                    "~/Content/vendors/moment/moment.js",
                    "~/Content/vendors/bootstrap-datepicker/js/bootstrap-datepicker.js",
                    "~/Content/vendors/bootstrap-daterangepicker/daterangepicker.js",
                    "~/Content/js/main.js",
                    "~/Content/vendors/intro.js/intro.js",
                    "~/Content/vendors/calendar/zabuto_calendar.min.js",
                    "~/Content/vendors/sco.message/sco.message.js",
                    "~/Content/vendors/intro.js/intro.js",
                    "~/Content/js/index.js"
                    )
                );

            bundles.Add(
                new ScriptBundle("~/Table/js")
                    .Include(
                    "~/Scripts/bootstrap-table.min.js",
                    "~/Scripts/tableExport.js",
                    "~/Scripts/bootstrap-editable.js",
                    "~/Scripts/bootstrap-table-editable.js",
                    "~/Scripts/bootstrap-table-export.js"
                    )
            );

            bundles.Add(
                new StyleBundle("~/Table/css")
                    .Include(
                    "~/Content/bootstrap-table.min.css"
                    )
                );
        }
    }
}