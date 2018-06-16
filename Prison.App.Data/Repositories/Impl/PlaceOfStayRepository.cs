using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.DataContext;
using System.Collections.Generic;

namespace Prison.App.Data.Repositories
{
    public class PlaceOfStayRepository: IPlaceOfStayRepository
    {
        private IPlaceDataContext _placeContext;

        public PlaceOfStayRepository(IPlaceDataContext placeContext)
        {
            ArgumentHelper.ThrowExceptionIfNull(placeContext, "IPlaceDataContext");

            _placeContext = placeContext;

        }

        public IReadOnlyCollection<PlaceOfStay> GetAllPlaces()
        {
            return _placeContext.GetAllPlaces();
        }

        public PlaceOfStay GetPlaceByID(int id)
        {
            return _placeContext.GetPlaceByID(id);
        }

        public void Create(PlaceOfStay emp)
        {
            _placeContext.Create(emp);
        }

        public void Update(PlaceOfStay emp)
        {
            _placeContext.Update(emp);
        }

        public void Delete(int id)
        {
            _placeContext.Delete(id);
        }
    }
}
