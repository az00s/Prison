using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Data.Repositories
{
    public interface IPlaceOfStayRepository
    {
        IEnumerable<PlaceOfStay> GetAllPlaces();
        PlaceOfStay GetPlaceByID(int id);
        void Create(PlaceOfStay plc);
        void Update(PlaceOfStay plc);
        void Delete(int id);
    }
}
