using System.Web;
using System.Web.Mvc;

namespace Prison.App.Business.Attributes
{
    public class UserAttribute: AuthorizeAttribute
    {
        //because of 'admin' role is including 'editor' and 'user' roles this attribute checks not only 'user', but editor and admin roles too.
        private string[] RoleArray = {"user","editor","admin"};

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var currentUser = HttpContext.Current.User;

            if (currentUser != null)
            {
                foreach (var role in RoleArray)
                {
                    if (currentUser.IsInRole(role))
                        return true;
                }

                return false;

            }

            return false;
        }
    }
}
