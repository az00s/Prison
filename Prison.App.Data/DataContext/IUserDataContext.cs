using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Data.DataContext
{
    public interface IUserDataContext
    {
        IEnumerable<User> GetAllUsers();
        User GetUserByID(int id);
        IEnumerable<string> GetAllLogins();
        IEnumerable<string> GetUserRoles(string login);
        string GetUserPasswordByLogin(string login);
        User GetUserByLogin(string login);
        IEnumerable<Employee> GetUnoccupiedEmployeeNames();
        void Create(User dtn);
        void Update(User dtn);
        void Delete(int id);
    }
}
