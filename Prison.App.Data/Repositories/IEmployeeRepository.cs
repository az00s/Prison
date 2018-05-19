using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Data.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllRecordsFromTable();
        Employee GetEmployeeByID(int id);
        void Create(Employee emp);
        void Update(Employee emp);
        void Delete(int id);
    }
}
