using Prison.App.Common.Entities;
using System;
using System.Collections.Generic;

namespace Prison.App.Data.DataContext
{
    public interface IDetaineeDataContext
    {
        IEnumerable<Detainee> GetAllDetainees();
        Detainee GetDetaineeByID(int id);
        IEnumerable<MaritalStatus> GetAllMaritalStatuses();
        IEnumerable<Detainee> GetDetaineesByDate(DateTime date);
        IEnumerable<Detainee> GetDetaineesByParams(string DetentionDate = null, string FirstName = null, string LastName = null, string MiddleName = null, string ResidenceAddress = null);
        void Create(Detainee dtn);
        void Update(Detainee dtn);
        void Delete(int id);
    }
}
