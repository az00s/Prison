using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Prison.App.Data.Repositories.Impl
{
    public class RoleRepository:IRoleRepository
    {
        private string _connection;

        private ILogger _log;

        public RoleRepository(ILogger log)
        {

            _connection = ConnectionStringHelper.GetConnectionString();
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");

            _log = log;
        }

        public IEnumerable<Role> GetAllRecordsFromTable()
        {
            List<Role> list = new List<Role>();
            SqlConnection conn = new SqlConnection(_connection);
            SqlCommand command = new SqlCommand("SelectAllRoles", conn) { CommandType = CommandType.StoredProcedure };
            SqlDataReader reader = null;

            try
            {
                conn.Open();

                reader = command.ExecuteReader();

                while (reader.Read())
                {

                    list.Add(new Role
                    {
                        RoleID = reader.GetInt32(0),
                        RoleName = reader.GetString(1),

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

        public Role GetRoleByID(int id)
        {
            Role role = null;
            SqlConnection conn = new SqlConnection(_connection);
            SqlCommand command = new SqlCommand("SelectRoleByID", conn) { CommandType = CommandType.StoredProcedure };
            command.Parameters.AddWithValue("@ID", id);
            SqlDataReader reader = null;

            try
            {
                conn.Open();

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    role = new Role
                    {
                        RoleID =reader.GetInt32(0),
                        RoleName =reader.GetString(1),

                    };
                }


                conn.Close();

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
                if (conn != null)
                {
                    conn.Dispose();
                }

                if (reader != null)
                {
                    reader.Dispose();
                }
            }

            return role;
        }


        public void Create(Role emp)
        {
            SqlConnection connection = new SqlConnection(_connection);
            SqlCommand command = new SqlCommand("CreateRole", connection) { CommandType = CommandType.StoredProcedure };

            try
            {
                connection.Open();

                SqlParameter[] parameters = {
                    new SqlParameter() { ParameterName = "@RoleName", Value = emp.RoleName },
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

        public void Update(Role emp)
        {
            SqlConnection connection = new SqlConnection(_connection);

            SqlCommand command = new SqlCommand("UpdateRole", connection) { CommandType = CommandType.StoredProcedure };

            try
            {
                connection.Open();

                SqlParameter[] parameters = {
                    new SqlParameter() { ParameterName = "@ID", Value = emp.RoleID },
                    new SqlParameter() { ParameterName = "@RoleName", Value = emp.RoleName },

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
            SqlCommand command = new SqlCommand("DeleteRole", connection) { CommandType = CommandType.StoredProcedure };

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

    }
}
