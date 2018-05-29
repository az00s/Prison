using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Data.Repositories;
using System.Collections.Generic;

namespace Prison.App.Business.Providers.Impl
{
    public class RoleProvider:IRoleProvider
    {
        private ILogger _log;

        private IRoleRepository _rep;

        public RoleProvider(ILogger log, IRoleRepository rep)
        {
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");
            ArgumentHelper.ThrowExceptionIfNull(rep, "IRoleRepository");

            _log = log;
            _rep = rep;
        }

        public IEnumerable<Role> GetAllRecordsFromTable()
        {
            return _rep.GetAllRecordsFromTable();
        }

        public Role GetRoleByID(int id)
        {
            return _rep.GetRoleByID(id);
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
