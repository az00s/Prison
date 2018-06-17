using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Data.DataContext
{
    public interface IPlaceDataContext
    {
        IReadOnlyCollection<PlaceOfStay> GetAllPlaces();
        PlaceOfStay GetPlaceByID(int id);
        void Create(PlaceOfStay place);
        void Update(PlaceOfStay place);
        void Delete(int id);
    }
}
