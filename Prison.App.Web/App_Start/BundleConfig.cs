using System.Web.Optimization;

namespace Prison.App.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Scripts/jquery.validate.min.js",
                    "~/Scripts/globalize.js",
                    "~/Scripts/globalize/date.js",
                    "~/Scripts/jquery.validate.globalize.min.js",
                    "~/Scripts/jquery.validate.unobtrusive.min.js"));
        }
    }
}