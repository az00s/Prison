using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Data.Repositories;
using System.Collections.Generic;

namespace Prison.App.Business.Providers.Impl
{
    public class RoleProvider:IRoleProvider
    {
        private IRoleRepository _rep;

        public RoleProvider(IRoleRepository rep)
        {
            ArgumentHelper.ThrowExceptionIfNull(rep, "IRoleRepository");

            _rep = rep;
        }

        public IReadOnlyCollection<Role> GetAllRoles()
        {
            return _rep.GetAllRoles();
        }

        public Role GetRoleByID(int id)
        {
            return _rep.GetRoleByID(id);
        }

    }
}
