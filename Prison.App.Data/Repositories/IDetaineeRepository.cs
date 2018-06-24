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
        IReadOnlyCollection<Detention> GetDetentionsForLast3Days();
        Detention GetLastDetention(int id);
        Release GetLastRelease(int id);
        Detention GetDetentionByID(int id);
        void ReleaseDetainee(Release release);
        void Create(Detainee dtn);
        void Update(Detainee dtn);
        void Delete(int id);
        IReadOnlyCollection<Detainee> Find(string DetentionDate, string FirstName, string LastName, string MiddleName, string ResidenceAddress);
    }
}
