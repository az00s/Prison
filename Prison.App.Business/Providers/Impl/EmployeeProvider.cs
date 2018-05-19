using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Data.Repositories;
using System.Collections.Generic;

namespace Prison.App.Business.Providers.Impl
{
    public class EmployeeProvider:IEmployeeProvider
    {
        private ILogger _log;

        private IEmployeeRepository _rep;

        public EmployeeProvider(ILogger log,IEmployeeRepository rep)
        {
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");
            ArgumentHelper.ThrowExceptionIfNull(rep, "IEmployeeRepository");

            _log = log;
            _rep = rep;
        }

        public IEnumerable<Employee> GetAllRecordsFromTable()
        {
            return _rep.GetAllRecordsFromTable();
        }
        public Employee GetEmployeeByID(int id)
        {
            return _rep.GetEmployeeByID(id);
        }

        public void Create(Employee emp)
        {
            _rep.Create(emp);
        }

        public void Update(Employee emp)
        {
            _rep.Update(emp);
        }

        public void Delete(int id)
        {
            _rep.Delete(id);
        }
    }
}
