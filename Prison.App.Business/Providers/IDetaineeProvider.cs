using Prison.App.Common.Entities;
using System;
using System.Collections.Generic;

namespace Prison.App.Business.Providers
{
    public interface IDetaineeProvider
    {
        IEnumerable<Detainee> GetAllRecordsFromTable();
        IEnumerable<Detainee> GetDetaineesByDate(DateTime date);
        Detainee GetDetaineeByID(int id);
        IEnumerable<MaritalStatus> GetAllMaritalStatusesFromTable();
        void Create(Detainee dtn);
        void Update(Detainee dtn);
        void Delete(int id);
    }
}
