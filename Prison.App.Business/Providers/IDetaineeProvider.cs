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
        Release GetLastRelease(int id);
        Release GetRelease(int detaineeID, int detentionID);
        IReadOnlyCollection<Detainee> Find(string DetentionDate, string FirstName, string LastName, string MiddleName, string ResidenceAddress);
    }
}
