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
        Detention GetLastDetention(int id);
        Detention GetDetentionByID(int id);
        IReadOnlyCollection<Detainee> Find(string DetentionDate=null, string FirstName = null, string LastName = null, string MiddleName = null, string ResidenceAddress = null);
    }
}
