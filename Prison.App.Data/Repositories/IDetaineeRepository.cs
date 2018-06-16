using Prison.App.Common.Entities;
using System;
using System.Collections.Generic;

namespace Prison.App.Data.Repositories
{
    public interface IDetaineeRepository
    {
        IReadOnlyCollection<Detainee> GetAllDetainees();
        Detainee GetDetaineeByID(int id);
        IReadOnlyCollection<Detainee> GetDetaineesByDate(DateTime date);
        IReadOnlyCollection<MaritalStatus> GetAllMaritalStatuses();
        IReadOnlyCollection<Detention> GetAllDetentions();
        Detention GetLastDetention(int id);
        Detention GetDetentionByID(int id);
        void ReleaseDetainee(Detention detention);
        void Create(Detainee dtn);
        void Update(Detainee dtn);
        void Delete(int id);
        IReadOnlyCollection<Detainee> GetDetaineesByParams(string DetentionDate=null, string FirstName = null, string LastName = null, string MiddleName = null, string ResidenceAddress = null);
    }
}
