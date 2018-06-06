using Prison.App.Common.Entities;

namespace Prison.App.Business.Services
{
    public interface IRoleService
    {
        void Create(Role emp);
        void Update(Role emp);
        void Delete(int id);

    }
}
