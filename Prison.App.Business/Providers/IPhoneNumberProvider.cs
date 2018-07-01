using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Business.Providers
{
    public interface IPhoneNumberProvider
    {
        IReadOnlyCollection<PhoneNumber> GetAllNumbers();
        PhoneNumber GetNumberByID(int id);
        IReadOnlyCollection<Detainee> GetAllDetaineeLastNames();
    }
}
