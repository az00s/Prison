using Prison.App.Common.Entities;

namespace Prison.App.Business.Services
{
    public interface IPhoneNumberService
    {
        void Create(PhoneNumber plc);
        void Update(PhoneNumber plc);
        void Delete(int id);
    }
}
