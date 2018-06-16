using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.DataContext;
using System.Collections.Generic;

namespace Prison.App.Data.Repositories.Impl
{
    public class RoleRepository:IRoleRepository
    {
        private IRoleDataContext _roleContext;

        public RoleRepository(IRoleDataContext roleContext)
        {
            ArgumentHelper.ThrowExceptionIfNull(roleContext, "IRoleDataContext");

            _roleContext = roleContext;
        }

        public IReadOnlyCollection<Role> GetAllRoles()
        {
            return _roleContext.GetAllRoles();
        }

        public Role GetRoleByID(int id)
        {
            return _roleContext.GetRoleByID(id);
        }

        public void Create(Role emp)
        {
            _roleContext.Create(emp);
        }

        public void Update(Role emp)
        {
            _roleContext.Update(emp);
        }

        public void Delete(int id)
        {
            _roleContext.Delete(id);
        }
    }
}
