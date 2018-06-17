using Prison.App.Common.Entities;

namespace Prison.App.Business.Services
{
    public interface IPositionService
    {
        void Create(Position pos);
        void Update(Position pos);
        void Delete(int id);
    }
}
