using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.Repositories;

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
