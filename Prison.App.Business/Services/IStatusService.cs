using Prison.App.Common.Entities;

namespace Prison.App.Business.Services
{
    public interface IStatusService
    {
        void Create(MaritalStatus emp);
        void Update(MaritalStatus emp);
        void Delete(int id);
    }
}
