using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.DataContext;
using System.Collections.Generic;

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

        public IReadOnlyCollection<User> GetAllUsers()
        {
            return _userContext.GetAllUsers();
        }

        public IReadOnlyCollection<string> GetAllLogins()
        {
            return _userContext.GetAllLogins();
        }

        public IReadOnlyCollection<string> GetUserRoles(string login)
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

        public IReadOnlyCollection<Employee> GetUnoccupiedEmployeeNames()
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
