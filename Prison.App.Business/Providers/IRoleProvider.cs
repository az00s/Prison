using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Business.Providers
{
    public interface IRoleProvider
    {
        IReadOnlyCollection<Role> GetAllRoles();
        Role GetRoleByID(int id);
    }
}
