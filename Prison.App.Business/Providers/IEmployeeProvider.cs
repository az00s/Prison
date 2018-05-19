using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Business.Providers
{
    public interface IEmployeeProvider
    {
        IEnumerable<Employee> GetAllRecordsFromTable();
        Employee GetEmployeeByID(int id);
        void Create(Employee emp);
        void Update(Employee emp);
        void Delete(int id);
    }
}
