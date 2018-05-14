using Prison.App.Common.Entities;
using Prison.App.Data.Repositories;
using Prison.App.Data.Repositories.Common;
using System.Collections.Generic;

namespace Prison.App.Data.Interfaces
{
    public interface IRepository
    {

        IDataCommonOperation<Detainee> Detainees { get; }
        ICollection<Detention> Detentions { get; set; }

        IDataCommonOperation<Employee> Employees { get;  }

        IDataCommonOperation<PlaceOfStay> PlacesOfStay { get; }



        void ErrorMethod();
    }
}
