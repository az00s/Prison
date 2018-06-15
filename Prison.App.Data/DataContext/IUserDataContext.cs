using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Data.DataContext
{
    public interface IUserDataContext
    {
        IReadOnlyCollection<User> GetAllUsers();
        User GetUserByID(int id);
        IReadOnlyCollection<string> GetAllLogins();
        IReadOnlyCollection<string> GetUserRoles(string login);
        string GetUserPasswordByLogin(string login);
        User GetUserByLogin(string login);
        IReadOnlyCollection<Employee> GetUnoccupiedEmployeeNames();
        void Create(User dtn);
        void Update(User dtn);
        void Delete(int id);
    }
}
