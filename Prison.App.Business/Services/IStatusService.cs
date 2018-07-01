using Prison.App.Common.Entities;

namespace Prison.App.Business.Services
{
    public interface IStatusService
    {
        void Create(MaritalStatus status);
        void Update(MaritalStatus status);
        void Delete(int id);
    }
}
