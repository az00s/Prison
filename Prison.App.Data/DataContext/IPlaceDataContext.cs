using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Data.DataContext
{
    public interface IPlaceDataContext
    {
        IReadOnlyCollection<PlaceOfStay> GetAllPlaces();
        PlaceOfStay GetPlaceByID(int id);
        void Create(PlaceOfStay dtn);
        void Update(PlaceOfStay dtn);
        void Delete(int id);
    }
}
