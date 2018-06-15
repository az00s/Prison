using Prison.App.Common.Entities;

namespace Prison.App.Business.Services
{
    public interface IEmployeeService
    {
        void Create(Employee emp);
        void Update(Employee emp);
        void Delete(int id);
    }
}
