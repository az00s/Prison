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
        IEnumerable<Detainee> GetDetaineesByParams(string DetentionDate=null, string FirstName = null, string LastName = null, string MiddleName = null, string ResidenceAddress = null);
    }
}
