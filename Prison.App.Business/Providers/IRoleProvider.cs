using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Business.Providers
{
    public interface IRoleProvider
    {
        IEnumerable<Role> GetAllRecordsFromTable();
        void Create(Role emp);
        void Update(Role emp);
        void Delete(int id);
        Role GetRoleByID(int id);
    }
}
