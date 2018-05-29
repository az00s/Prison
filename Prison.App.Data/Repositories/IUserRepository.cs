using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Data.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllRecordsFromTable();

        IEnumerable<string> GetAllLogins();

        IEnumerable<string> GetUserRoles(string login);
        IEnumerable<Employee> GetUnoccupiedEmployeeNames();
        string GetUserPasswordByLogin(string login);

        User GetUserByLogin(string login);
        User GetUserByID(int id);

        void Create(User emp);

        void Update(User emp);
        void Delete(int id);

    }
}
