using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.DataContext;
using System.Collections.Generic;

namespace Prison.App.Data.Repositories
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private IEmployeeDataContext _employeeContext;

        public EmployeeRepository(IEmployeeDataContext employeeContext)
        {
            ArgumentHelper.ThrowExceptionIfNull(employeeContext, "IEmployeeDataContext");

            _employeeContext = employeeContext;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeContext.GetAllEmployees();
        }

        public Employee GetEmployeeByID(int id)
        {
            return _employeeContext.GetEmployeeByID(id);
        }

        public void Create(Employee emp)
        {
            _employeeContext.Create(emp);
        }

        public void Update(Employee emp)
        {
            _employeeContext.Update(emp);
        }

        public void Delete(int id)
        {
            _employeeContext.Delete(id);
        }
    }
}
