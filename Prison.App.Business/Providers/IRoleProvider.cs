using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Business.Providers
{
    public interface IRoleProvider
    {
        IEnumerable<Role> GetAllRoles();
        Role GetRoleByID(int id);
    }
}
