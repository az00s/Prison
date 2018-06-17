using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Prison.App.Data.DataContext.Impl
{
    internal class UserDataContext:IUserDataContext
    {
        private readonly IDataContext<User> _context;

        public UserDataContext(IDataContext<User> context)
        {
            ArgumentHelper.ThrowExceptionIfNull(context, "IDataContext<User>");

            _context = context;
        }

        public IReadOnlyCollection<User> GetAllUsers()
        {
            var dataSet = _context.ExecuteQuery("SelectAllUsers", null, CommandType.StoredProcedure);

            var userList = ToUserList(dataSet);

            return userList;
        }

        public User GetUserByID(int id)
        {
            var parameters = new Dictionary<string, object> { { "@ID", id } };

            var dataSet = _context.ExecuteQuery("SelectUserByID", parameters, CommandType.StoredProcedure);

            var user = ToUser(dataSet);

            return user;

        }

        public IReadOnlyCollection<string> GetAllLogins()
        {
            var dataSet = _context.ExecuteQuery("SelectAllLogins", null, CommandType.StoredProcedure);

            var loginList = ToStringList(dataSet);

            return loginList;
        }

        public IReadOnlyCollection<string> GetUserRoles(string login)
        {
            var parameters = new Dictionary<string, object> { { "@Login", login } };

            var dataSet = _context.ExecuteQuery("SelectAllUserRoles", parameters, CommandType.StoredProcedure);

            var userRoles = ToStringList(dataSet);

            return userRoles;
        }

        public string GetUserPasswordByLogin(string login)
        {
            var parameters = new Dictionary<string, object> { { "@Login", login } };

            var dataSet = _context.ExecuteQuery("SelectPasswordByLogin", parameters, CommandType.StoredProcedure);

            var password = dataSet.Tables[0].Rows[0].Field<string>(0);

            return password;
        }

        public User GetUserByLogin(string login)
        {
            var parameters = new Dictionary<string, object> { { "@Login", login } };

            var dataSet = _context.ExecuteQuery("SelectUserByLogin", parameters, CommandType.StoredProcedure);

            var user = ToUser(dataSet);

            return user;
        }

        public IReadOnlyCollection<Employee> GetUnoccupiedEmployeeNames()
        {
            var dataSet = _context.ExecuteQuery("SelectUnoccupiedEmployeeNames", null, CommandType.StoredProcedure);

            var empList = ToEmployeeList(dataSet);

            return empList;
        }

        public void Create(User usr)
        {
            //get id numbers of user roles
            var RoleIDs = usr.Roles.Select(r => r.RoleID).Distinct();

            //create dictionary for parameters
            var parametersDictionary =
                new Dictionary<string, object>
                {
                    { "@ID", usr.UserID },
                    { "@Email", usr.Email },
                    { "@Password", usr.Password },
                };

            //create complex parameter, which correlates with custom type in db "RoleIdTable"
            //stored procedure "CreateUser" needs this complex parameter
            var customParameter = _context.CreateCustomParameter("@Table", RoleIDs, "RoleID", SqlDbType.Structured, "RoleIdTable");

            //get all simpleparameters
            var parameters = _context.GetParameterList(parametersDictionary);

            //get all parameters
            parameters.Add(customParameter);

            _context.ExecuteNonQuery("CreateUser", parameters.ToArray(), CommandType.StoredProcedure);
        }

        public void Update(User usr)
        {
            //get id numbers of user roles
            var RoleIDs = usr.Roles.Select(r => r.RoleID).Distinct();

            //create dictionary for parameters
            var parametersDictionary =
                new Dictionary<string, object>
                {
                    { "@ID", usr.UserID },
                    { "@Email", usr.Email },
                    { "@Password", usr.Password },
                };

            //create complex parameter, which correlates with custom type in db "RoleIdTable"
            //stored procedure "CreateUser" needs this complex parameter
            var customParameter = _context.CreateCustomParameter("@Table", RoleIDs, "RoleID", SqlDbType.Structured, "RoleIdTable");

            //get all simpleparameters
            var parameters = _context.GetParameterList(parametersDictionary);

            //get all parameters
            parameters.Add(customParameter);

            _context.ExecuteNonQuery("UpdateUser", parameters.ToArray(), CommandType.StoredProcedure);
        }

        public void Delete(int id)
        {
            var parameters =
                new Dictionary<string, object>
                {
                    { "@ID", id },
                };

            _context.ExecuteNonQuery("DeleteUser", parameters, CommandType.StoredProcedure);
        }

        #region Converters
        private IReadOnlyCollection<User> ToUserList(DataSet dataset)
        {
            return dataset.Tables[0].AsEnumerable().Select(row=>
                new User
                {
                    UserID = row.Field<int>("UserID"),
                    UserName = row.Field<string>("UserName"),
                    Email = row.Field<string>("Email"),
                    Password = row.Field<string>("Password"),
                }
            ).ToList();
        }

        private IReadOnlyCollection<Employee> ToEmployeeList(DataSet dataset)
        {
            return dataset.Tables[0].AsEnumerable().Select(row=>
                new Employee
                {
                    EmployeeID = row.Field<int>(0),
                    LastName = row.Field<string>(1),

                }
            ).ToList();
        }

        private IReadOnlyCollection<string> ToStringList(DataSet dataset)
        {
            return dataset.Tables[0].AsEnumerable().Select(row=> row.Field<string>(0)).ToList();
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
            var UserRoles = new Role[table.Rows.Count];

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
