using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.DataContext;
using System;
using System.Collections.Generic;

namespace Prison.App.Data.Repositories
{
    public class DetaineeRepository: IDetaineeRepository
    {
        private IDetaineeDataContext _detaineeContext;

        public DetaineeRepository(IDetaineeDataContext detaineeContext)
        {
            ArgumentHelper.ThrowExceptionIfNull(detaineeContext, "IDetaineeDataContext");

            _detaineeContext = detaineeContext;
        }

        public Detainee GetDetaineeByID(int id)
        {
            return _detaineeContext.GetDetaineeByID(id);
        }

        public IEnumerable<Detainee> GetAllDetainees()
        {
           return _detaineeContext.GetAllDetainees();
        }

        public IEnumerable<Detention> GetAllDetentions()
        {
            return _detaineeContext.GetAllDetentions();
        }

        public IEnumerable<Detainee> GetDetaineesByDate(DateTime date)
        {
            return _detaineeContext.GetDetaineesByDate(date);
        }

        public IEnumerable<MaritalStatus> GetAllMaritalStatuses()
        {
           return _detaineeContext.GetAllMaritalStatuses();
        }

        public IEnumerable<Detainee> GetDetaineesByParams(string DetentionDate=null, string FirstName = null, string LastName = null, string MiddleName = null, string ResidenceAddress = null)
        {
            return _detaineeContext.Find(DetentionDate, FirstName, LastName,MiddleName,ResidenceAddress);
        }

        public void ReleaseDetainee(Detention detention)
        {
            _detaineeContext.ReleaseDetainee(detention);
        }

        public Detention GetLastDetention(int id)
        {
            return _detaineeContext.GetLastDetention(id);
        }

        public Detention GetDetentionByID(int id)
        {
            return _detaineeContext.GetDetentionByID(id);
        }

        public void Create(Detainee dtn)
        {
            _detaineeContext.Create(dtn);

        }

        public void Update(Detainee dtn)
        {
            _detaineeContext.Update(dtn);
        }

        public void Delete(int id)
        {
            _detaineeContext.Delete(id);
        }

    }
}
