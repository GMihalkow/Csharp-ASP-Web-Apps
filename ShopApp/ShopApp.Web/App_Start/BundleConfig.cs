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
		}

		private static void RegisterStyles(BundleCollection bundles)
		{
			bundles.Add(new StyleBundle("~/Content/style.css").Include(
				"~/Content/style.css"));

			bundles.Add(new StyleBundle("~/bundles/jquery-ui").Include(
				"~/Content/jqueryUI/jquery-ui.css",
				"~/Content/jqueryUI/jquery-ui-structure.css",
				"~/Content/jqueryUI/jquery-ui-theme.css"));
		}
	}
}