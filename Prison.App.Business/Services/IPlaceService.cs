using Prison.App.Common.Entities;

namespace Prison.App.Business.Services
{
    public interface IPlaceService
    {
        void Create(PlaceOfStay plc);
        void Update(PlaceOfStay plc);
        void Delete(int id);

    }
}
