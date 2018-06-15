using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Business.Providers
{
    public interface IEmployeeProvider
    {
        IReadOnlyCollection<Employee> GetAllEmployees();
        Employee GetEmployeeByID(int id);
    }
}
