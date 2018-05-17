using Prison.App.Common.Entities;
using Prison.App.Data.Repositories.Common;

namespace Prison.App.Business.Providers
{
    public interface IDataProvider
    {
        IDataCommonOperation<Detainee> Detainees { get; }
        IDataCommonOperation<Employee> Employees { get; }
        IDataCommonOperation<PlaceOfStay> PlacesOfStay { get; }
    }
}
