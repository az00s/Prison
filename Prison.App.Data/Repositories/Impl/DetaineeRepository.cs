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

        public IReadOnlyCollection<Detainee> GetAllDetainees()
        {
           return _detaineeContext.GetAllDetainees();
        }

        public IReadOnlyCollection<Detention> GetAllDetentions()
        {
            return _detaineeContext.GetAllDetentions();
        }

        public IReadOnlyCollection<Detention> GetDetentionsForLast3Days()
        {
            return _detaineeContext.GetDetentionsForLast3Days();
        }

        public IReadOnlyCollection<Detainee> GetDetaineesByDate(DateTime date)
        {
            return _detaineeContext.GetDetaineesByDate(date);
        }

        public IReadOnlyCollection<MaritalStatus> GetAllMaritalStatuses()
        {
           return _detaineeContext.GetAllMaritalStatuses();
        }

        public IReadOnlyCollection<Detainee> Find(string DetentionDate, string FirstName, string LastName, string MiddleName, string ResidenceAddress)
        { 
            return _detaineeContext.Find(DetentionDate, FirstName, LastName,MiddleName,ResidenceAddress);
        }

        public void ReleaseDetainee(Release release)
        {
            _detaineeContext.ReleaseDetainee(release);
        }

        public Detention GetLastDetention(int id)
        {
            return _detaineeContext.GetLastDetention(id);
        }

        public Release GetLastRelease(int id)
        {
            return _detaineeContext.GetLastRelease(id);
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
