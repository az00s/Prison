using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Business.Providers
{
    public interface IUserProvider
    {
        IReadOnlyCollection<User> GetAllUsers();

        IReadOnlyCollection<string> GetAllLogins();

        IReadOnlyCollection<string> GetUserRoles(string user);

        string GetUserPasswordByLogin(string user);
        IReadOnlyCollection<Employee> GetUnoccupiedEmployeeNames();

        User GetUserByLogin(string login);

        User GetUserByID(int id);

    }
}
