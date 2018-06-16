using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Data.Repositories
{
    public interface IRoleRepository
    {
        IReadOnlyCollection<Role> GetAllRoles();
        void Create(Role emp);
        void Update(Role emp);
        void Delete(int id);
        Role GetRoleByID(int id);
    }
}
