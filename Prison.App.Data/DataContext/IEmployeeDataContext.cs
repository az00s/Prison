using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Data.DataContext
{
    public interface IEmployeeDataContext
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeByID(int id);
        void Create(Employee dtn);
        void Update(Employee dtn);
        void Delete(int id);
    }
}
