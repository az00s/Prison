using Prison.App.Common.Entities;

namespace Prison.App.Business.Services
{
    public interface IPositionService
    {
        void Create(Position emp);
        void Update(Position emp);
        void Delete(int id);
    }
}
