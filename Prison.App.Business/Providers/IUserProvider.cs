using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Business.Providers
{
    public interface IUserProvider
    {
        IEnumerable<User> GetAllUsers();

        IEnumerable<string> GetAllLogins();

        IEnumerable<string> GetUserRoles(string user);

        string GetUserPasswordByLogin(string user);
        IEnumerable<Employee> GetUnoccupiedEmployeeNames();

        User GetUserByLogin(string login);

        User GetUserByID(int id);

    }
}
