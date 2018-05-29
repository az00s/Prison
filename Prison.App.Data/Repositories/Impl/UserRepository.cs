using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prison.App.Data.Repositories.Impl
{
    public class UserRepository:IUserRepository
    {
        private string _connection;

        private ILogger _log;

        public UserRepository(ILogger log)
        {

            _connection = ConnectionStringHelper.GetConnectionStringFromConfig();
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");

            _log = log;
        }

        public IEnumerable<User> GetAllRecordsFromTable()
        {
            List<User> list = new List<User>();
            SqlConnection conn = new SqlConnection(_connection);
            SqlCommand command = new SqlCommand("SelectAllUsers", conn) { CommandType = CommandType.StoredProcedure };
            SqlDataReader reader = null;

            try
            {
                conn.Open();

                reader = command.ExecuteReader();

                while (reader.Read())
                {

                    list.Add(new User
                    {
                        UserID = reader.GetInt32(0),
                        UserName=reader.GetString(1),
                        Email = reader.GetString(2),
                        Password = reader.GetString(3),

                    });
                }

                reader.Close();


                conn.Close();

            }
            catch (InvalidOperationException ex)
            {
                _log.Error(ex.Message);
                list = null;
            }

            catch (SqlException ex)
            {
                _log.Error(ex.Message);
                list = null;
            }

            catch (InvalidCastException ex)
            {
                _log.Error(ex.Message);
                list = null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Dispose();
                }

                if (reader != null)
                {
                    reader.Dispose();
                }
            }

            return list;
        }

        public IEnumerable<string> GetAllLogins()
        {
            List<string> list = new List<string>();
            SqlConnection conn = new SqlConnection(_connection);
            SqlCommand command = new SqlCommand("SelectAllLogins", conn) { CommandType = CommandType.StoredProcedure };
            SqlDataReader reader = null;

            try
            {
                conn.Open();

                reader = command.ExecuteReader();

                while (reader.Read())
                {

                    list.Add(reader.GetString(0));
                }

                reader.Close();


                conn.Close();

            }
            catch (InvalidOperationException ex)
            {
                _log.Error(ex.Message);
                list = null;
            }

            catch (SqlException ex)
            {
                _log.Error(ex.Message);
                list = null;
            }

            catch (InvalidCastException ex)
            {
                _log.Error(ex.Message);
                list = null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Dispose();
                }

                if (reader != null)
                {
                    reader.Dispose();
                }
            }

            return list;
        }

        public IEnumerable<string> GetUserRoles(string login)
        {
            List<string> list = new List<string>();
            SqlConnection conn = new SqlConnection(_connection);
            SqlCommand command = new SqlCommand("SelectAllUserRoles", conn) { CommandType = CommandType.StoredProcedure };
            SqlDataReader reader = null;

            try
            {
                conn.Open();
                command.Parameters.AddWithValue("@Login", login);
                reader = command.ExecuteReader();

                while (reader.Read())
                {

                    list.Add(reader.GetString(0));
                }

                reader.Close();


                conn.Close();

            }
            catch (InvalidOperationException ex)
            {
                _log.Error(ex.Message);
                list = null;
            }

            catch (SqlException ex)
            {
                _log.Error(ex.Message);
                list = null;
            }

            catch (InvalidCastException ex)
            {
                _log.Error(ex.Message);
                list = null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Dispose();
                }

                if (reader != null)
                {
                    reader.Dispose();
                }
            }

            return list;
        }

        public string GetUserPasswordByLogin(string login)
        {
            string password = null;
            SqlConnection conn = new SqlConnection(_connection);
            SqlCommand command = new SqlCommand("SelectPasswordByLogin", conn) { CommandType = CommandType.StoredProcedure };
            SqlDataReader reader = null;

            try
            {

                conn.Open();
                command.Parameters.AddWithValue("@Login", login);
                reader = command.ExecuteReader();

                while (reader.Read())
                {

                    password = reader.GetString(0);
                }

                reader.Close();


                conn.Close();

            }
            catch (InvalidOperationException ex)
            {
                _log.Error(ex.Message);
                password = null;
            }

            catch (SqlException ex)
            {
                _log.Error(ex.Message);
                password = null;
            }

            catch (InvalidCastException ex)
            {
                _log.Error(ex.Message);
                password = null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Dispose();
                }

                if (reader != null)
                {
                    reader.Dispose();
                }
            }

            return password;
        }

        public User GetUserByLogin(string login)
        {
            User user = null;
            SqlConnection conn = new SqlConnection(_connection);
            SqlCommand command = new SqlCommand("SelectUserByLogin", conn) { CommandType = CommandType.StoredProcedure };
            command.Parameters.AddWithValue("@Login", login);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataset = new DataSet();
            
            try
            {

                if (adapter != null)
                {
                    conn.Open();
                    adapter.Fill(dataset);
                }

                var UserData = dataset.Tables[0];
                var RoleData = dataset.Tables[1];

                user=new User
                {
                    UserID = UserData.Rows[0].Field<int>("UserID"),
                    Email = UserData.Rows[0].Field<string>("Email"),
                    Password = UserData.Rows[0].Field<string>("Password"),
                    UserName = UserData.Rows[0].Field<string>("UserName"),
                    Roles = GetUserRoles(RoleData),

                };
                

                conn.Close();

            }
            catch (InvalidOperationException ex)
            {
                _log.Error(ex.Message);
                user = null;
            }

            catch (SqlException ex)
            {
                _log.Error(ex.Message);
                user = null;
            }

            catch (InvalidCastException ex)
            {
                _log.Error(ex.Message);
                user = null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Dispose();
                }

                if (adapter != null)
                {
                    adapter.Dispose();
                }
            }

            return user;
        }

        public User GetUserByID(int id)
        {
            User user = null;
            SqlConnection conn = new SqlConnection(_connection);
            SqlCommand command = new SqlCommand("SelectUserByID", conn) { CommandType = CommandType.StoredProcedure };
            command.Parameters.AddWithValue("@ID", id);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataset = new DataSet();

            try
            {

                if (adapter != null)
                {
                    conn.Open();
                    adapter.Fill(dataset);
                }

                var UserData = dataset.Tables[0];
                var RoleData = dataset.Tables[1];

                user = new User
                {
                    UserID = UserData.Rows[0].Field<int>("UserID"),
                    Email = UserData.Rows[0].Field<string>("Email"),
                    Password = UserData.Rows[0].Field<string>("Password"),
                    UserName = UserData.Rows[0].Field<string>("UserName"),
                    Roles = GetUserRoles(RoleData),

                };


                conn.Close();

            }
            catch (InvalidOperationException ex)
            {
                _log.Error(ex.Message);
                user = null;
            }

            catch (SqlException ex)
            {
                _log.Error(ex.Message);
                user = null;
            }

            catch (InvalidCastException ex)
            {
                _log.Error(ex.Message);
                user = null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Dispose();
                }

                if (adapter != null)
                {
                    adapter.Dispose();
                }
            }

            return user;
        }

        public IEnumerable<Employee> GetUnoccupiedEmployeeNames()
        {
            List<Employee> list = new List<Employee>();
            SqlConnection conn = new SqlConnection(_connection);
            SqlCommand command = new SqlCommand("SelectUnoccupiedEmployeeNames", conn) { CommandType = CommandType.StoredProcedure };
            SqlDataReader reader = null;

            try
            {
                conn.Open();

                reader = command.ExecuteReader();

                while (reader.Read())
                {

                    list.Add(new Employee
                        {
                            EmployeeID = reader.GetInt32(0),
                            LastName= reader.GetString(1)
                    });
                }

                reader.Close();


                conn.Close();

            }
            catch (InvalidOperationException ex)
            {
                _log.Error(ex.Message);
                list = null;
            }

            catch (SqlException ex)
            {
                _log.Error(ex.Message);
                list = null;
            }

            catch (InvalidCastException ex)
            {
                _log.Error(ex.Message);
                list = null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Dispose();
                }

                if (reader != null)
                {
                    reader.Dispose();
                }
            }

            return list;
        }




        public void Create(User emp)
        {
            SqlConnection connection = new SqlConnection(_connection);
            SqlCommand command = new SqlCommand("CreateUser", connection) { CommandType = CommandType.StoredProcedure };

            IEnumerable<int> RoleIDs = emp.Roles.Select(r => r.RoleID);
            
            try
            {
                connection.Open();

                SqlParameter[] parameters = {
                    new SqlParameter() { ParameterName = "@ID", Value = emp.UserID },
                    new SqlParameter() { ParameterName = "@Email", Value = emp.Email },
                     new SqlParameter() { ParameterName = "@Password", Value = emp.Password },

                     new SqlParameter()
                     {
                         ParameterName = "@Table",
                         Value = CreateDataTable(RoleIDs),
                         SqlDbType =SqlDbType.Structured,
                         TypeName ="RoleIdTable"
                     },
                };


                command.Parameters.AddRange(parameters);

                command.ExecuteNonQuery();

                connection.Close();
            }

            catch (InvalidOperationException ex)
            {
                _log.Error(ex.Message);
            }

            catch (SqlException ex)
            {
                _log.Error(ex.Message);
            }
            catch (InvalidCastException ex)
            {
                _log.Error(ex.Message);
            }

            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
            }
        }

        public void Update(User emp)
        {
            SqlConnection connection = new SqlConnection(_connection);

            SqlCommand command = new SqlCommand("UpdateUser", connection) { CommandType = CommandType.StoredProcedure };

            IEnumerable<int> RoleIDs = emp.Roles.Select(r => r.RoleID).Distinct();

            try
            {
                connection.Open();

                SqlParameter[] parameters = {
                    new SqlParameter() { ParameterName = "@ID", Value = emp.UserID },
                    new SqlParameter() { ParameterName = "@Email", Value = emp.Email },
                    new SqlParameter() { ParameterName = "@Password", Value = emp.Password },
                    new SqlParameter()
                     {
                         ParameterName = "@Table",
                         Value = CreateDataTable(RoleIDs),
                         SqlDbType =SqlDbType.Structured,
                         TypeName ="RoleIdTable"
                     },

                };

                command.Parameters.AddRange(parameters);

                command.ExecuteNonQuery();

                connection.Close();
            }
            catch (InvalidOperationException ex)
            {
                _log.Error(ex.Message);
            }

            catch (SqlException ex)
            {
                _log.Error(ex.Message);
            }
            catch (InvalidCastException ex)
            {
                _log.Error(ex.Message);
            }

            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
            }

        }

        public void Delete(int id)
        {
            SqlConnection connection = new SqlConnection(_connection);
            SqlCommand command = new SqlCommand("DeleteUser", connection) { CommandType = CommandType.StoredProcedure };

            try
            {
                connection.Open();

                SqlParameter[] parameters = {
                    new SqlParameter() { ParameterName = "@ID", Value = id }
                };

                command.Parameters.AddRange(parameters);

                command.ExecuteNonQuery();

                connection.Close();
            }

            catch (InvalidOperationException ex)
            {
                _log.Error(ex.Message);
            }

            catch (SqlException ex)
            {
                _log.Error(ex.Message);
            }
            catch (InvalidCastException ex)
            {
                _log.Error(ex.Message);
            }

            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
            }

        }

        private Role[] GetUserRoles(DataTable table)
        {
            Role[] UserRoles = new Role[table.Rows.Count];

            for (int i=0;i< UserRoles.Length;i++)
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

        private static DataTable CreateDataTable(IEnumerable<int> ids)
        {
            DataTable table = new DataTable();
            table.Columns.Add("RoleID", typeof(int));
            foreach (int id in ids)
            {
                table.Rows.Add(id);
            }
            return table;
        }


    }
}
