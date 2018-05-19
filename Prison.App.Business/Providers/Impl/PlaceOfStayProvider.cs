using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Data.Repositories;
using System.Collections.Generic;

namespace Prison.App.Business.Providers.Impl
{
    public class PlaceOfStayProvider:IPlaceOfStayProvider
    {
        private ILogger _log;

        private IPlaceOfStayRepository _rep;

        public PlaceOfStayProvider(ILogger log, IPlaceOfStayRepository rep)
        {
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");
            ArgumentHelper.ThrowExceptionIfNull(rep, "IPlaceOfStayRepository");

            _log = log;
            _rep = rep;
        }

        public IEnumerable<PlaceOfStay> GetAllRecordsFromTable()
        {
            return _rep.GetAllRecordsFromTable();
        }

        public PlaceOfStay GetPlaceOfStayByID(int id)
        {
            return _rep.GetPlaceOfStayByID(id);
        }

        public void Create(PlaceOfStay plc)
        {
            _rep.Create(plc);
        }

        public void Update(PlaceOfStay plc)
        {
            _rep.Update(plc);
        }

        public void Delete(int id)
        {
            _rep.Delete(id);
        }
    }
}
