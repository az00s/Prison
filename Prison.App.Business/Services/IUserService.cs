using Prison.App.Common.Entities;

namespace Prison.App.Business.Services
{
    public interface IUserService
    {
        void Create(User plc);

        void Update(User plc);

        void Delete(int id);

    }
}
