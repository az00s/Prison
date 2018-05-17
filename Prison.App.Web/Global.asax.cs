using Prison.App.Web;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Prison
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ViewEngines.Engines.Add(new PrisonViewEngine());

        }
    }

    public class PrisonViewEngine : RazorViewEngine
    {

        private static string[] NewPartialViewFormats = new[] {
        
        "~/Views/Shared/Partials/{0}.cshtml"
    };

        public PrisonViewEngine()
        {
            base.PartialViewLocationFormats = base.PartialViewLocationFormats.Union(NewPartialViewFormats).ToArray();
        }

    }
}
