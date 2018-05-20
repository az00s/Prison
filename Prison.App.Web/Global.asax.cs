using Prison.App.Common.Interfaces;
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



    //adding new path for finding Partial Views in folder Shared/"Partials"
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
