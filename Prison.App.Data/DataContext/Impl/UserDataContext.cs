using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Prison.App.Data.DataContext.Impl
{
    internal class UserDataContext:IUserDataContext
    {
        private IDataContext<User> _context;

        public UserDataContext(IDataContext<User> context)
        {
            ArgumentHelper.ThrowExceptionIfNull(context, "IDataContext<User>");

            _context = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> userList = new List<User>();

            var dataSet = _context.ExecuteQuery("SelectAllUsers", null, CommandType.StoredProcedure);

            userList = ToUserList(dataSet);

            return userList;
        }

        public User GetUserByID(int id)
        {
            User user;

            IDictionary<string, object> parameters = new Dictionary<string, object> { { "@ID", id } };

            var dataSet = _context.ExecuteQuery("SelectUserByID", parameters, CommandType.StoredProcedure);

            user = ToUser(dataSet);

            return user;

        }

        public IEnumerable<string> GetAllLogins()
        {
            IEnumerable<string> loginList = new List<string>();

            var dataSet = _context.ExecuteQuery("SelectAllLogins", null, CommandType.StoredProcedure);

            loginList = ToStringList(dataSet);

            return loginList;
        }

        public IEnumerable<string> GetUserRoles(string login)
        {
            IEnumerable<string> userRoles = new List<string>();

            IDictionary<string, object> parameters = new Dictionary<string, object> { { "@Login", login } };

            var dataSet = _context.ExecuteQuery("SelectAllUserRoles", parameters, CommandType.StoredProcedure);

            userRoles = ToStringList(dataSet);

            return userRoles;
        }

        public string GetUserPasswordByLogin(string login)
        {
            string password;

            IDictionary<string, object> parameters = new Dictionary<string, object> { { "@Login", login } };

            var dataSet = _context.ExecuteQuery("SelectPasswordByLogin", parameters, CommandType.StoredProcedure);

            password = dataSet.Tables[0].Rows[0].Field<string>(0);

            return password;
        }

        public User GetUserByLogin(string login)
        {
            User user;

            IDictionary<string, object> parameters = new Dictionary<string, object> { { "@Login", login } };

            var dataSet = _context.ExecuteQuery("SelectUserByLogin", parameters, CommandType.StoredProcedure);

            user = ToUser(dataSet);

            return user;
        }

        public IEnumerable<Employee> GetUnoccupiedEmployeeNames()
        {
            IEnumerable<Employee> empList = new List<Employee>();

            var dataSet = _context.ExecuteQuery("SelectUnoccupiedEmployeeNames", null, CommandType.StoredProcedure);

            empList = ToEmployeeList(dataSet);

            return empList;
        }

        public void Create(User usr)
        {
            //get id numbers of user roles
            IEnumerable<int> RoleIDs = usr.Roles.Select(r => r.RoleID).Distinct();

            //create dictionary for parameters
            IDictionary<string, object> parametersDictionary =
                new Dictionary<string, object>
                {
                    { "@ID", usr.UserID },
                    { "@Email", usr.Email },
                    { "@Password", usr.Password },
                };

            //create complex parameter, which correlates with custom type in db "RoleIdTable"
            //stored procedure "CreateUser" needs this complex parameter
            var customParameter = _context.CreateCustomParameter("@Table", RoleIDs,SqlDbType.Structured, "RoleIdTable");

            //get all simpleparameters
            var parameters = _context.GetParameterList(parametersDictionary);

            //get all parameters
            parameters.Add(customParameter);

            _context.ExecuteNonQuery("CreateUser", parameters.ToArray(), CommandType.StoredProcedure);
        }

        public void Update(User usr)
        {
            //get id numbers of user roles
            IEnumerable<int> RoleIDs = usr.Roles.Select(r => r.RoleID).Distinct();

            //create dictionary for parameters
            IDictionary<string, object> parametersDictionary =
                new Dictionary<string, object>
                {
                    { "@ID", usr.UserID },
                    { "@Email", usr.Email },
                    { "@Password", usr.Password },
                };

            //create complex parameter, which correlates with custom type in db "RoleIdTable"
            //stored procedure "CreateUser" needs this complex parameter
            var customParameter = _context.CreateCustomParameter("@Table", RoleIDs, SqlDbType.Structured, "RoleIdTable");

            //get all simpleparameters
            var parameters = _context.GetParameterList(parametersDictionary);

            //get all parameters
            parameters.Add(customParameter);

            _context.ExecuteNonQuery("UpdateUser", parameters.ToArray(), CommandType.StoredProcedure);
        }

        public void Delete(int id)
        {
            IDictionary<string, object> parameters =
                new Dictionary<string, object>
                {
                    { "@ID", id },
                };

            _context.ExecuteNonQuery("DeleteUser", parameters, CommandType.StoredProcedure);
        }



        #region Converters
        private IEnumerable<User> ToUserList(DataSet dataset)
        {
            List<User> list = new List<User>();

            var userTable = dataset.Tables[0];

            foreach (var row in userTable.AsEnumerable())
            {
                list.Add(new User
                {
                    UserID = row.Field<int>("UserID"),
                    UserName = row.Field<string>("UserName"),
                    Email = row.Field<string>("Email"),
                    Password = row.Field<string>("Password"),
                });
            }
            return list;
        }

        private IEnumerable<Employee> ToEmployeeList(DataSet dataset)
        {
            List<Employee> list = new List<Employee>();

            var empTable = dataset.Tables[0];

            foreach (var row in empTable.AsEnumerable())
            {
                list.Add(new Employee
                {
                    EmployeeID = row.Field<int>(0),
                    LastName = row.Field<string>(1),
                    
                });
            }
            return list;
        }

        private IEnumerable<string> ToStringList(DataSet dataset)
        {
            List<string> list = new List<string>();

            var loginTable = dataset.Tables[0];

            foreach (var row in loginTable.AsEnumerable())
            {
                list.Add(row.Field<string>(0));
            }
            return list;
        }


        private User ToUser(DataSet dataset)
        {
            var row = dataset.Tables[0].Rows[0];
            var roleData = dataset.Tables[1];
            return new User
            {
                UserID = row.Field<int>("UserID"),
                UserName = row.Field<string>("UserName"),
                Email = row.Field<string>("Email"),
                Password = row.Field<string>("Password"),
                Roles = GetUserRoles(roleData)
            };
        }

        private Role[] GetUserRoles(DataTable table)
        {
            Role[] UserRoles = new Role[table.Rows.Count];

            for (int i = 0; i < UserRoles.Length; i++)
            {
                var row = table.Rows[i];

                UserRoles[i] = new Role
                {
                    RoleID = row.Field<int>("RoleID"),
                    RoleName = row.Field<string>("RoleName"),
                };

            }
            return UserRoles;
        }


        #endregion
    }
}
