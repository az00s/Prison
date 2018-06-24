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
        IReadOnlyCollection<Detention> GetDetentionsForLast3Days();
        IReadOnlyCollection<Detainee> Find(string DetentionDate, string FirstName, string LastName, string MiddleName, string ResidenceAddress);
        Detention GetLastDetention(int id);
        Release GetLastRelease(int id);
        void ReleaseDetainee(Release release);
        Detention GetDetentionByID(int id);
        void Create(Detainee dtn);
        void Update(Detainee dtn);
        void Delete(int id);
    }
}
