using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Data.DataContext
{
    public interface IRoleDataContext
    {
        IReadOnlyCollection<Role> GetAllRoles();
        Role GetRoleByID(int id);
        void Create(Role role);
        void Update(Role role);
        void Delete(int id);
    }
}
