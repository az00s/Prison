using Prison.App.Web;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Newtonsoft.Json;
using Prison.App.Business.Services;
using Prison.App.Common.Entities;
using Prison.App.Web.DependencyResolution;
using Prison.App.Common.Interfaces;

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

        //this method  is called every time when user make a request
        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            //get cookie from user request
            var cookie = HttpContext.Current.Request.Cookies.Get(FormsAuthentication.FormsCookieName);

            if (cookie != null)
            {       
                    //decrypt the cookie
                    var decryptedCookie = FormsAuthentication.Decrypt(cookie.Value);
                    //get User from cookie
                    var User = JsonConvert.DeserializeObject<User>(decryptedCookie.UserData);
                    //build IPrincipal impl for putting it in context
                    var userPrincipal = new UserPrincipal(User);
                    //put IPrincipal impl in context
                    HttpContext.Current.User = userPrincipal;
            }
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();
            Server.ClearError();
            Response.Clear();

            string errorType = "ServerError";

            using (var container = IoC.Initialize())
            {
                var log = container.GetInstance<ILogger>();
                log.Error(exc.Message,exc);
            }

            Response.Redirect($"~/Error/{errorType}");
        }

    }

    //adding new path for finding Partial Views in folder Shared/"Partials"
    public class PrisonViewEngine : RazorViewEngine
    {

        private static string[] NewPartialViewFormats = new[] {"~/Views/Shared/Partials/{0}.cshtml"};

        public PrisonViewEngine()
        {
            base.PartialViewLocationFormats = base.PartialViewLocationFormats.Union(NewPartialViewFormats).ToArray();
        }

    }

    
}
