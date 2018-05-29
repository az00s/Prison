using System.Web;
using System.Web.Mvc;

namespace Prison.App.Business.Attributes
{
    public class EditorAttribute:AuthorizeAttribute
    {
        //because of 'admin' role is including 'editor' role this attribute checks not only 'editor', but admin role too.
        private string[] RoleArray = {"editor", "admin" };

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
