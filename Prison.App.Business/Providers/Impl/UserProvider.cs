using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Data.Repositories;
using System.Collections.Generic;

namespace Prison.App.Business.Providers.Impl
{
    public class UserProvider:IUserProvider
    {
        private IUserRepository _rep;

        public UserProvider(ILogger log, IUserRepository rep)
        {
            ArgumentHelper.ThrowExceptionIfNull(rep, "IUserRepository");

            _rep = rep;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _rep.GetAllUsers();
        }

        public IEnumerable<string> GetAllLogins()
        {
            return _rep.GetAllLogins();
        }

        public IEnumerable<string> GetUserRoles(string user)
        {
            return _rep.GetUserRoles(user);
        }

        public string GetUserPasswordByLogin(string user)
        {
            return _rep.GetUserPasswordByLogin(user);
        }

        public User GetUserByLogin(string login)
        {
            return _rep.GetUserByLogin(login);
        }

        public IEnumerable<Employee> GetUnoccupiedEmployeeNames()
        {
            return _rep.GetUnoccupiedEmployeeNames();
        }

        public User GetUserByID(int id)
        {
            return _rep.GetUserByID(id);
        }


    }
}
