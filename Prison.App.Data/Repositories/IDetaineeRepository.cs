using Prison.App.Common.Entities;
using System;
using System.Collections.Generic;

namespace Prison.App.Data.Repositories
{
    public interface IDetaineeRepository
    {
        IEnumerable<Detainee> GetAllRecordsFromTable();
        Detainee GetDetaineeByID(int id);
        IEnumerable<Detainee> GetDetaineesByDate(DateTime date);
        IEnumerable<MaritalStatus> GetAllMaritalStatusesFromTable();
        void Create(Detainee dtn);
        void Update(Detainee dtn);
        void Delete(int id);
    }
}
