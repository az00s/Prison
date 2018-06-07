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
    public class PositionRepository:IPositionRepository
    {
        
            private string _connection;

            private ILogger _log;

            public PositionRepository(ILogger log)
            {

                _connection = ConnectionStringHelper.GetConnectionString();
                ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");

                _log = log;
            }

            public IEnumerable<Position> GetAllRecordsFromTable()
            {
                List<Position> list = new List<Position>();
                SqlConnection conn = new SqlConnection(_connection);
                SqlCommand command = new SqlCommand("SelectAllPositions", conn) { CommandType = CommandType.StoredProcedure };
                SqlDataReader reader = null;

                try
                {
                    conn.Open();

                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        list.Add(new Position
                        {
                            PositionID = reader.GetInt32(0),
                            PositionName = reader.GetString(1),

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

            //public Position GetPositionByID(int id)
            //{
            //    Employee employee = null;

            //    SqlConnection conn = new SqlConnection(_connection);
            //    SqlCommand command = new SqlCommand("SelectEmployeeByID", conn) { CommandType = CommandType.StoredProcedure };
            //    SqlDataReader reader = null;

            //    try
            //    {
            //        conn.Open();

            //        command.Parameters.AddWithValue("@ID", id);

            //        reader = command.ExecuteReader();

            //        if (reader.Read())
            //        {
            //            employee = new Employee
            //            {
            //                EmployeeID = reader.GetInt32(0),
            //                FirstName = reader.GetString(1),
            //                LastName = reader.GetString(2),
            //                MiddleName = reader[3].ToString(),
            //                PositionID = reader.GetInt32(4),
            //            };
            //        }

            //        reader.Close();

            //        conn.Close();
            //    }
            //    catch (InvalidOperationException ex)
            //    {
            //        _log.Error(ex.Message);
            //        employee = null;
            //    }

            //    catch (SqlException ex)
            //    {
            //        _log.Error(ex.Message);
            //        employee = null;
            //    }

            //    catch (InvalidCastException ex)
            //    {
            //        _log.Error(ex.Message);
            //        employee = null;
            //    }

            //    finally
            //    {
            //        if (conn != null)
            //        {
            //            conn.Dispose();
            //        }

            //        if (reader != null)
            //        {
            //            reader.Dispose();
            //        }
            //    }

            //    return employee;
            //}


            public void Create(Employee emp)
            {
                SqlConnection connection = new SqlConnection(_connection);
                SqlCommand command = new SqlCommand("CreateEmployee", connection) { CommandType = CommandType.StoredProcedure };

                try
                {
                    connection.Open();

                    SqlParameter[] parameters = {
                    new SqlParameter() { ParameterName = "@FirstName", Value = emp.FirstName },
                    new SqlParameter() { ParameterName = "@LastName", Value = emp.LastName },
                    new SqlParameter() { ParameterName = "@MiddleName", Value = emp.MiddleName },
                    new SqlParameter() { ParameterName = "@PositionID", Value = emp.PositionID },
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

            public void Update(Employee emp)
            {
                SqlConnection connection = new SqlConnection(_connection);

                SqlCommand command = new SqlCommand("UpdateEmployee", connection) { CommandType = CommandType.StoredProcedure };

                try
                {
                    connection.Open();

                    SqlParameter[] parameters = {
                    new SqlParameter() { ParameterName = "@ID", Value = emp.EmployeeID },
                    new SqlParameter() { ParameterName = "@FirstName", Value = emp.FirstName },
                    new SqlParameter() { ParameterName = "@LastName", Value = emp.LastName },
                    new SqlParameter() { ParameterName = "@MiddleName", Value = emp.MiddleName??"" },
                    new SqlParameter() { ParameterName = "@PositionID", Value = emp.PositionID },

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
                SqlCommand command = new SqlCommand("DeleteEmployee", connection) { CommandType = CommandType.StoredProcedure };

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
