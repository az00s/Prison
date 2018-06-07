using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Common.Loggers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Prison.App.Data.Repositories
{
    public class PlaceOfStayRepository: IPlaceOfStayRepository
    {
        private string _connection;

        private ILogger _log;

        public PlaceOfStayRepository(Logger log)
        {
            _connection = ConnectionStringHelper.GetConnectionString();
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");

            _log = log;
        }

        public IEnumerable<PlaceOfStay> GetAllRecordsFromTable()
        {
            List<PlaceOfStay> list = new List<PlaceOfStay>();

            SqlConnection conn = new SqlConnection(_connection);
            SqlCommand command = new SqlCommand("SelectAllPlacesOfStay", conn) { CommandType=CommandType.StoredProcedure};
            SqlDataReader reader = null;

            try
            {
                conn.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {

                    list.Add(new PlaceOfStay
                    {
                        PlaceID = reader.GetInt32(0),
                        Address = reader.GetString(1),
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

        

        public PlaceOfStay GetPlaceOfStayByID(int id)
        {
            PlaceOfStay place = null;

            SqlConnection conn = new SqlConnection(_connection);
            SqlCommand command = new SqlCommand("SelectPlaceOfStayByID", conn) { CommandType = CommandType.StoredProcedure };
            SqlDataReader reader = null;

            try
            {
                conn.Open();

                command.Parameters.AddWithValue("@ID",id);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    place = new PlaceOfStay
                    {
                        PlaceID = reader.GetInt32(0),
                        Address = reader.GetString(1),
                    };
                }
                
                reader.Close();

                conn.Close();
            }
            catch (InvalidOperationException ex)
            {
                _log.Error(ex.Message);
                place = null;
            }

            catch (SqlException ex)
            {
                _log.Error(ex.Message);
                place = null;
            }

            catch (InvalidCastException ex)
            {
                _log.Error(ex.Message);
                place = null;
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

            return place;
        }

        public void Create(PlaceOfStay emp)
        {
            SqlConnection connection = new SqlConnection(_connection);
            var command = new SqlCommand("CreatePlaceOfStay", connection) { CommandType = CommandType.StoredProcedure };
            try
            {
                connection.Open();

                SqlParameter[] parameters = {
                    new SqlParameter() { ParameterName = "@Address", Value = emp.Address }
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

        public void Update(PlaceOfStay emp)
        {
            SqlConnection connection = new SqlConnection(_connection);
            SqlCommand command = new SqlCommand("UpdatePlaceOfStay", connection) { CommandType=CommandType.StoredProcedure};

            try
            {
                connection.Open();


                SqlParameter[] parameters = {
                    new SqlParameter() { ParameterName = "@ID", Value = emp.PlaceID },
                    new SqlParameter() { ParameterName = "@Address", Value = emp.Address }
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
            SqlCommand command = new SqlCommand("DeletePlaceOfStay", connection) { CommandType=CommandType.StoredProcedure};

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
