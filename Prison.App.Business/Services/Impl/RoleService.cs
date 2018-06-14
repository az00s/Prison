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

        public void Create(Role emp)
        {
            _rep.Create(emp);
        }
        public void Update(Role emp)
        {
            _rep.Update(emp);
        }
        public void Delete(int id)
        {
            _rep.Delete(id);
        }

    }
}
