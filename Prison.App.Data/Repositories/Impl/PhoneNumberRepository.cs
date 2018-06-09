using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.DataContext;
using System.Collections.Generic;

namespace Prison.App.Data.Repositories.Impl
{
    public class PhoneNumberRepository:IPhoneNumberRepository
    {
        private IPhoneNumberDataContext _numberContext;

        public PhoneNumberRepository(IPhoneNumberDataContext numberContext)
        {
            ArgumentHelper.ThrowExceptionIfNull(numberContext, "IPhoneNumberDataContext");

            _numberContext = numberContext;

        }

        public IEnumerable<PhoneNumber> GetAllNumbers()
        {
            return _numberContext.GetAllNumbers();
        }

        public PhoneNumber GetNumberByID(int id)
        {
            return _numberContext.GetNumberByID(id);
        }

        public void Create(PhoneNumber emp)
        {
            _numberContext.Create(emp);
        }

        public void Update(PhoneNumber emp)
        {
            _numberContext.Update(emp);
        }

        public void Delete(int id)
        {
            _numberContext.Delete(id);
        }
    }
}
