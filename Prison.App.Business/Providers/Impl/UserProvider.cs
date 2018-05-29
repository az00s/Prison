using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prison.App.Business.Providers.Impl
{
    public class UserProvider:IUserProvider
    {
        private ILogger _log;

        private IUserRepository _rep;

        public UserProvider(ILogger log, IUserRepository rep)
        {
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");
            ArgumentHelper.ThrowExceptionIfNull(rep, "IUserRepository");

            _log = log;
            _rep = rep;
        }

        public IEnumerable<User> GetAllRecordsFromTable()
        {
            return _rep.GetAllRecordsFromTable();
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

        public void Create(User plc)
        {
            _rep.Create(plc);
        }

        public void Update(User plc)
        {
            _rep.Update(plc);
        }

        public void Delete(int id)
        {
            _rep.Delete(id);
        }

    }
}
