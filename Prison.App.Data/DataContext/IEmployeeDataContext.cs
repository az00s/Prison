using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Data.DataContext
{
    public interface IEmployeeDataContext
    {
        IReadOnlyCollection<Employee> GetAllEmployees();
        Employee GetEmployeeByID(int id);
        void Create(Employee emp);
        void Update(Employee emp);
        void Delete(int id);
    }
}
