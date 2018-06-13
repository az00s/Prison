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
        IEnumerable<Detention> GetAllDetentions();
        IEnumerable<Detainee> Find(string DetentionDate = null, string FirstName = null, string LastName = null, string MiddleName = null, string ResidenceAddress = null);
        Detention GetLastDetention(int id);
        void ReleaseDetainee(Detention detention);
        Detention GetDetentionByID(int id);
        void Create(Detainee dtn);
        void Update(Detainee dtn);
        void Delete(int id);
    }
}
