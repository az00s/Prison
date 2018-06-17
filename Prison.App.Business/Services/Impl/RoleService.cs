using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.Repositories;

namespace Prison.App.Business.Services.Impl
{
    public class RoleService: IRoleService
    {
        private IRoleRepository _rep;

        public RoleService(IRoleRepository rep)
        {
            ArgumentHelper.ThrowExceptionIfNull(rep, "IRoleRepository");

            _rep = rep;
        }

        public void Create(Role role)
        {
            _rep.Create(role);
        }

        public void Update(Role role)
        {
            _rep.Update(role);
        }

        public void Delete(int id)
        {
            _rep.Delete(id);
        }
    }
}
