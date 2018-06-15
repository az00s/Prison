using System.Web;
using System.Web.Mvc;

namespace Prison.App.Web.Attributes
{
    public class AdminAttribute : AuthorizeAttribute
    {

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var currentUser = HttpContext.Current.User;

            if (currentUser != null)
            {
                return currentUser.IsInRole("admin");
            }

            return false;
        }

    }
}
