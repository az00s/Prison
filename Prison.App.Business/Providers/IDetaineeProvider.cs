using Prison.App.Common.Entities;
using System;
using System.Collections.Generic;

namespace Prison.App.Business.Providers
{
    public interface IDetaineeProvider
    {
        IReadOnlyCollection<Detainee> GetAllDetainees();
        IReadOnlyCollection<Detainee> GetDetaineesByDate(DateTime date);
        Detainee GetDetaineeByID(int id);
        IReadOnlyCollection<MaritalStatus> GetAllMaritalStatuses();
        IReadOnlyCollection<Detention> GetAllDetentions();
        IReadOnlyCollection<Detention> GetDetentionsForLast3Days();
        Detention GetLastDetention(int id);
        Release GetLastRelease(int id);
        Detention GetDetentionByID(int id);
        IReadOnlyCollection<Detainee> Find(string DetentionDate, string FirstName, string LastName, string MiddleName, string ResidenceAddress);
    }
}
