using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.Repositories;
using System;

namespace Prison.App.Business.Services.Impl
{
    public class UserService: IUserService
    {
        private IUserRepository _rep;

        public UserService(IUserRepository rep)
        {
            ArgumentHelper.ThrowExceptionIfNull(rep, "IUserRepository");

            _rep = rep;
        }

        public void Create(User user)
        {
            _rep.Create(user);
        }

        public void Update(User user)
        {
            _rep.Update(user);
        }

        public void Delete(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                _rep.Delete(id);
            }
            else
            {
                throw new ArgumentException($"Идентификатор пользователя указан неверно.Пожалуйста укажите значение от 0 до {int.MaxValue}");
            }
        }
    }
}
