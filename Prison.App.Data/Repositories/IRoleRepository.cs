using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Data.Repositories
{
    public interface IRoleRepository
    {
        IReadOnlyCollection<Role> GetAllRoles();
        void Create(Role role);
        void Update(Role role);
        void Delete(int id);
        Role GetRoleByID(int id);
    }
}
