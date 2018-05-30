using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Data.Repositories;
using System;
using System.Collections.Generic;

namespace Prison.App.Business.Providers.Impl
{
    public class DetaineeProvider: IDetaineeProvider
    {
        private ILogger _log;

        private IDetaineeRepository _rep;

        public DetaineeProvider(ILogger log, IDetaineeRepository rep)
        {
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");
            ArgumentHelper.ThrowExceptionIfNull(rep, "IDetaineeRepository");

            _log = log;
            _rep = rep;
        }

        public IEnumerable<Detainee> GetAllRecordsFromTable()
        {
            return _rep.GetAllRecordsFromTable();
        }

        public Detainee GetDetaineeByID(int id)
        {
            return _rep.GetDetaineeByID(id);
        }

        public IEnumerable<Detainee> GetDetaineesByDate(DateTime date)
        {
            return _rep.GetDetaineesByDate(date);
        }

        public IEnumerable<MaritalStatus> GetAllMaritalStatusesFromTable()
        {
            return _rep.GetAllMaritalStatusesFromTable();
        }
        public void Create(Detainee dtn)
        {
            _rep.Create(dtn);
        }

        public void Update(Detainee dtn)
        {
            _rep.Update(dtn);
        }

        public void Delete(int id)
        {
            _rep.Delete(id);
        }

        public IEnumerable<Detainee> GetDetaineesByParams(string DetentionDate=null, string FirstName = null, string LastName = null, string MiddleName = null, string ResidenceAddress = null)
        {
            return _rep.GetDetaineesByParams(DetentionDate, FirstName, LastName, MiddleName, ResidenceAddress);
        }
    }
}
