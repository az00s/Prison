using Prison.App.Common.Entities;

namespace Prison.App.Business.Services
{
    public interface IUserService
    {
        void Create(User user);

        void Update(User user);

        void Delete(int id);
    }
}
