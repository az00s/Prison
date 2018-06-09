using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Business.Providers
{
    public interface IPhoneNumberProvider
    {
        IEnumerable<PhoneNumber> GetAllNumbers();
        PhoneNumber GetNumberByID(int id);
        IEnumerable<Detainee> GetAllDetaineeLastNames();
    }
}
