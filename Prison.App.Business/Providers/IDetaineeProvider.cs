using Prison.App.Common.Entities;
using System;
using System.Collections.Generic;

namespace Prison.App.Business.Providers
{
    public interface IDetaineeProvider
    {
        IEnumerable<Detainee> GetAllDetainees();
        IEnumerable<Detainee> GetDetaineesByDate(DateTime date);
        Detainee GetDetaineeByID(int id);
        IEnumerable<MaritalStatus> GetAllMaritalStatuses();
       
        IEnumerable<Detainee> GetDetaineesByParams(string DetentionDate=null, string FirstName = null, string LastName = null, string MiddleName = null, string ResidenceAddress = null);
    }
}
