using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Data.Repositories
{
    public interface IUserRepository
    {
        IReadOnlyCollection<User> GetAllUsers();

        IReadOnlyCollection<string> GetAllLogins();

        IReadOnlyCollection<string> GetUserRoles(string login);

        IReadOnlyCollection<Employee> GetUnoccupiedEmployeeNames();

        string GetUserPasswordByLogin(string login);

        User GetUserByLogin(string login);

        User GetUserByID(int id);

        void Create(User user);

        void Update(User user);

        void Delete(int id);
    }
}
