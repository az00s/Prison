using Prison.App.Common.Entities;
using System;
using System.Collections.Generic;

namespace Prison.App.Data.DataContext
{
    public interface IDetaineeDataContext
    {
        IReadOnlyCollection<Detainee> GetAllDetainees();
        Detainee GetDetaineeByID(int id);
        IReadOnlyCollection<MaritalStatus> GetAllMaritalStatuses();
        IReadOnlyCollection<Detainee> GetDetaineesByDate(DateTime date);
        IReadOnlyCollection<Detention> GetAllDetentions();
        IReadOnlyCollection<Detainee> Find(string DetentionDate, string FirstName, string LastName, string MiddleName, string ResidenceAddress);
        Detention GetLastDetention(int id);
        void ReleaseDetainee(Detention detention);
        Detention GetDetentionByID(int id);
        void Create(Detainee dtn);
        void Update(Detainee dtn);
        void Delete(int id);
    }
}
