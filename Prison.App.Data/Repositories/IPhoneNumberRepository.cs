using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Data.Repositories
{
    public interface IPhoneNumberRepository
    {
        IReadOnlyCollection<PhoneNumber> GetAllNumbers();
        PhoneNumber GetNumberByID(int id);
        IReadOnlyCollection<Detainee> GetAllDetaineeLastNames();
        void Create(PhoneNumber emp);
        void Update(PhoneNumber emp);
        void Delete(int id);
    }
}
