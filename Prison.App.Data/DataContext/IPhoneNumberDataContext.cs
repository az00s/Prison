using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Data.DataContext
{
    public interface IPhoneNumberDataContext
    {
        IEnumerable<PhoneNumber> GetAllNumbers();
        PhoneNumber GetNumberByID(int id);
        IEnumerable<Detainee> GetAllDetaineeLastNames();
        void Create(PhoneNumber dtn);
        void Update(PhoneNumber dtn);
        void Delete(int id);
    }
}
