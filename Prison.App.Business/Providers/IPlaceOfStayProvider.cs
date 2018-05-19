using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Business.Providers
{
    public interface IPlaceOfStayProvider
    {
        IEnumerable<PlaceOfStay> GetAllRecordsFromTable();
        PlaceOfStay GetPlaceOfStayByID(int id);
        void Create(PlaceOfStay plc);
        void Update(PlaceOfStay plc);
        void Delete(int id);
    }
}
