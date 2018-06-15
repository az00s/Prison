using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Prison.App.Data.DataContext.Impl
{
    internal class EmployeeDataContext:IEmployeeDataContext
    {
        private IDataContext<Employee> _context;

        public EmployeeDataContext(IDataContext<Employee> context)
        {
            ArgumentHelper.ThrowExceptionIfNull(context, "IDataContext<Employee>");

            _context = context;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            IEnumerable<Employee> employeeList = new List<Employee>();

            var dataSet = _context.ExecuteQuery("SelectAllEmployees", null, CommandType.StoredProcedure);

            employeeList = ToEmployeeList(dataSet);

            return employeeList;
        }

        public Employee GetEmployeeByID(int id)
        {
            Employee employee;

            IDictionary<string, object> parameters = new Dictionary<string, object> { { "@ID", id } };

            var dataSet = _context.ExecuteQuery("SelectEmployeeByID", parameters, CommandType.StoredProcedure);

            employee = ToEmployee(dataSet);

            return employee;

        }

        public void Create(Employee dtn)
        {
            IDictionary<string, object> parameters =
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
            IDictionary<string, object> parameters =
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
            IDictionary<string, object> parameters =
                new Dictionary<string, object>
                {
                    { "@ID", id },
                };

            _context.ExecuteNonQuery("DeleteEmployee", parameters, CommandType.StoredProcedure);
        }



        #region Converters
        private IEnumerable<Employee> ToEmployeeList(DataSet dataset)
        {
            List<Employee> list = new List<Employee>();

            var employeeTable = dataset.Tables[0];

            foreach (var row in employeeTable.AsEnumerable())
            {
                list.Add(new Employee
                {
                    EmployeeID = row.Field<int>("EmployeeID"),
                    FirstName = row.Field<string>("FirstName"),
                    LastName = row.Field<string>("LastName"),
                    MiddleName = row.Field<string>("MiddleName"),
                    PositionID = row.Field<int>("PositionID")
                });
            }
            return list;
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
