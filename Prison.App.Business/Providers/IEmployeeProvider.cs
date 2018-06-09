using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Business.Providers
{
    public interface IEmployeeProvider
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeByID(int id);
        
    }
}
