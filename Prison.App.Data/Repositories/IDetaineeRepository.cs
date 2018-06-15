using Prison.App.Common.Entities;
using System;
using System.Collections.Generic;

namespace Prison.App.Data.Repositories
{
    public interface IDetaineeRepository
    {
        IEnumerable<Detainee> GetAllDetainees();
        Detainee GetDetaineeByID(int id);
        IEnumerable<Detainee> GetDetaineesByDate(DateTime date);
        IEnumerable<MaritalStatus> GetAllMaritalStatuses();
        IEnumerable<Detention> GetAllDetentions();
        Detention GetLastDetention(int id);
        Detention GetDetentionByID(int id);
        void ReleaseDetainee(Detention detention);
        void Create(Detainee dtn);
        void Update(Detainee dtn);
        void Delete(int id);
        IEnumerable<Detainee> GetDetaineesByParams(string DetentionDate=null, string FirstName = null, string LastName = null, string MiddleName = null, string ResidenceAddress = null);
    }
}
