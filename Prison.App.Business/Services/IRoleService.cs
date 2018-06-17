using Prison.App.Common.Entities;

namespace Prison.App.Business.Services
{
    public interface IRoleService
    {
        void Create(Role role);
        void Update(Role role);
        void Delete(int id);

    }
}
