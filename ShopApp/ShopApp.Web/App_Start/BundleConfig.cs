using System.Web.Optimization;

namespace ShopApp.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterStyles(bundles);
            RegisterScripts(bundles);
        }

        private static void RegisterScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modal").Include(
                "~/scripts/common/modal.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/moment.js").Include(
                "~/scripts/moment.js/moment.min.js",
                "~/scripts/moment.js/moment-with-locales.min.js"
            ));
        }

        private static void RegisterStyles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/style.css").Include(
                "~/Content/style.css"));

            bundles.Add(new StyleBundle("~/Content/flatpickr.css").Include(
                "~/Content/flatpickr/themes/dark.css",
                "~/Content/flatpickr/ie.css",
                "~/Content/flatpickr/flatpickr.min.css"));
        }
    }
}