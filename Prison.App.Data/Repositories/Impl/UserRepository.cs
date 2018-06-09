using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Data.DataContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prison.App.Data.Repositories.Impl
{
    public class UserRepository:IUserRepository
    {
        private IUserDataContext _userContext;

        public UserRepository(IUserDataContext userContext)
        {
            ArgumentHelper.ThrowExceptionIfNull(userContext, "IUserDataContext");

            _userContext = userContext;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userContext.GetAllUsers();
        }

        public IEnumerable<string> GetAllLogins()
        {
            return _userContext.GetAllLogins();
        }

        public IEnumerable<string> GetUserRoles(string login)
        {
            return _userContext.GetUserRoles(login);
        }

        public string GetUserPasswordByLogin(string login)
        {
            return _userContext.GetUserPasswordByLogin(login);
        }

        public User GetUserByLogin(string login)
        {
            return _userContext.GetUserByLogin(login);
        }

        public User GetUserByID(int id)
        {
            return _userContext.GetUserByID(id);
        }

        public IEnumerable<Employee> GetUnoccupiedEmployeeNames()
        {
            return _userContext.GetUnoccupiedEmployeeNames();
        }

        public void Create(User emp)
        {
            _userContext.Create(emp);
        }

        public void Update(User emp)
        {
            _userContext.Update(emp);
        }

        public void Delete(int id)
        {
            _userContext.Delete(id);
        }
    }
}
