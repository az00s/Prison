using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.Interfaces;
using Prison.App.Data.Repositories;
using Prison.App.Data.Repositories.Common;

namespace Prison.App.Business.Providers
{
    public class DataProvider:IDataProvider 
    {
        private IRepository _rep;

        public DataProvider(IRepository rep)
        {
            ArgumentHelper.ThrowExceptionIfNull(rep, "IRepository");
            _rep = rep;
        }

        public IDataCommonOperation<Detainee> Detainees { get { return _rep.Detainees; } }

        public IDataCommonOperation<Employee> Employees { get { return _rep.Employees; } }

        public IDataCommonOperation<PlaceOfStay> PlacesOfStay { get { return _rep.PlacesOfStay; } }
    }
}
