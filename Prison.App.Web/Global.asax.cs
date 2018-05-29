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
