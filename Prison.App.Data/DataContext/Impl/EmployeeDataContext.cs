using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Prison.App.Data.DataContext.Impl
{
    internal class EmployeeDataContext : IEmployeeDataContext
    {
        private readonly IDataContext<Employee> _context;

        public EmployeeDataContext(IDataContext<Employee> context)
        {
            ArgumentHelper.ThrowExceptionIfNull(context, "IDataContext<Employee>");

            _context = context;
        }

        public IReadOnlyCollection<Employee> GetAllEmployees()
        {
            var dataSet = _context.ExecuteQuery("SelectAllEmployees", null, CommandType.StoredProcedure);

            var employeeList = ToEmployeeList(dataSet);

            return employeeList;
        }

        public Employee GetEmployeeByID(int id)
        {
            var parameters = new Dictionary<string, object> { { "@ID", id } };

            var dataSet = _context.ExecuteQuery("SelectEmployeeByID", parameters, CommandType.StoredProcedure);

            var employee = ToEmployee(dataSet);

            return employee;
        }

        public void Create(Employee dtn)
        {
            var parameters =
                new Dictionary<string, object>
                {
                    { "@FirstName", dtn.FirstName },
                    { "@LastName", dtn.LastName },
                    { "@MiddleName", dtn.MiddleName??"" },
                    { "@PositionID", dtn.PositionID },
                };

            _context.ExecuteNonQuery("CreateEmployee", parameters, CommandType.StoredProcedure);
        }

        public void Update(Employee dtn)
        {
            var parameters =
                 new Dictionary<string, object>
                 {
                     { "@ID", dtn.EmployeeID },
                    { "@FirstName", dtn.FirstName },
                    { "@LastName", dtn.LastName },
                    { "@MiddleName", dtn.MiddleName??"" },
                    { "@PositionID", dtn.PositionID },
                 };

            _context.ExecuteNonQuery("UpdateEmployee", parameters, CommandType.StoredProcedure);
        }

        public void Delete(int id)
        {
            var parameters =
                new Dictionary<string, object>
                {
                    { "@ID", id },
                };

            _context.ExecuteNonQuery("DeleteEmployee", parameters, CommandType.StoredProcedure);
        }

        #region Converters
        private IReadOnlyCollection<Employee> ToEmployeeList(DataSet dataset)
        {
           return dataset.Tables[0].AsEnumerable().Select(x =>
                new Employee
                {
                    EmployeeID = x.Field<int>("EmployeeID"),
                    FirstName = x.Field<string>("FirstName"),
                    LastName = x.Field<string>("LastName"),
                    MiddleName = x.Field<string>("MiddleName"),
                    PositionID = x.Field<int>("PositionID")
                }

            ).ToList();

        }

        private Employee ToEmployee(DataSet dataset)
        {
            var row = dataset.Tables[0].Rows[0];

            return new Employee
            {
                EmployeeID = row.Field<int>("EmployeeID"),
                FirstName = row.Field<string>("FirstName"),
                LastName = row.Field<string>("LastName"),
                MiddleName = row.Field<string>("MiddleName"),
                PositionID = row.Field<int>("PositionID")

            };
        }

        #endregion

    }
}
