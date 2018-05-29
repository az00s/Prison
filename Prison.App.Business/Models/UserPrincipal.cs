using Prison.App.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Prison.App.Business.Services
{
    public class UserPrincipal : IPrincipal
    {
        public UserPrincipal(User user)
        {
            UserName = user.UserName;
            Roles = user.Roles;
            Identity = new GenericIdentity(user.UserName);
        }
        public string UserName { get; set; }

        public Role[] Roles { get; set; }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return Roles.Select(r=>r.RoleName).Contains(role);
        }
    }
}
