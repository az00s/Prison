﻿using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Common.Loggers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Prison.App.Data.Repositories
{
    public class DetaineeRepository: IDetaineeRepository
    {
        private string _connection;

        private ILogger _log;

        public DetaineeRepository(Logger log)
        {
            _connection = ConnectionStringHelper.GetConnectionStringFromConfig();
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");

            _log = log;
        }

        public Detainee GetDetaineeByID(int id)
        {
            Detainee detainee = null;

            SqlConnection conn = new SqlConnection(_connection);
            SqlCommand command = new SqlCommand("SelectDetaineeByID", conn) { CommandType = CommandType.StoredProcedure };
            SqlDataReader reader = null;

            try
            {
                conn.Open();

                command.Parameters.AddWithValue("@ID", id);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    detainee = new Detainee
                    {
                        DetaineeID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        MiddleName = reader[3].ToString(),
                        BirstDate = reader.GetDateTime(4),
                        MaritalStatusID = reader.GetInt32(5),
                        WorkPlace = reader.GetString(6),
                        ImagePath = reader.GetString(7),
                        ResidenceAddress = reader.GetString(8),
                        AdditionalData = reader[9].ToString(),
                        Detentions = GetDetentionsByDetaineeID(reader.GetInt32(0))
                    };
                }

                reader.Close();

                conn.Close();
            }
            catch (InvalidOperationException ex)
            {
                _log.Error(ex.Message);
                detainee = null;
            }

            catch (SqlException ex)
            {
                _log.Error(ex.Message);
                detainee = null;
            }

            catch (InvalidCastException ex)
            {
                _log.Error(ex.Message);
                detainee = null;
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

            return detainee;
        }


        IEnumerable<Detention> GetDetentionsByDetaineeID(int id)
        {
            List<Detention> list = new List<Detention>();
            SqlConnection conn = new SqlConnection(_connection);
            SqlDataReader reader = null;
            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("SelectDetentionsByDetaineeID", conn) { CommandType = CommandType.StoredProcedure };

                command.Parameters.Add(new SqlParameter() { ParameterName = "@ID", Value = id });

                reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Detention
                        {
                            DetentionID = reader.GetInt32(0),
                            DetentionDate = reader.GetDateTime(1),
                            DetainedByWhomID = reader.GetInt32(2),
                            DeliveryDate = reader.GetDateTime(3),
                            DeliveredByWhomID = reader.GetInt32(4),
                            ReleasеDate = reader.GetDateTime(5),
                            ReleasedByWhomID = reader.GetInt32(6),
                            PlaceID = reader.GetInt32(7),
                            AmountForStaying = reader.GetDecimal(8),
                            PaidAmount = reader.GetDecimal(9),
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

        public IEnumerable<Detainee> GetAllRecordsFromTable()
        {
            List<Detainee> list = new List<Detainee>();

            SqlConnection conn = new SqlConnection(_connection);
            SqlCommand command = new SqlCommand("SelectAllDetainees", conn) { CommandType=CommandType.StoredProcedure};
            //SqlDataReader reader = null;

            //------------------
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataset = new DataSet();
            
            try
            {
                if (adapter != null)
                {
                    conn.Open();
                    adapter.Fill(dataset);
                }

                var DetaineeTable = dataset.Tables[0];
                var DetentionTable = dataset.Tables[1];

                foreach (var row in DetaineeTable.AsEnumerable())
                {
                    list.Add(new Detainee
                    {
                        DetaineeID = (int)row["DetaineeID"],
                        FirstName = row["FirstName"].ToString(),
                        LastName = row["LastName"].ToString(),
                        MiddleName = row["MiddleName"].ToString(),
                        BirstDate = (DateTime)row["BirstDate"],
                        MaritalStatusID = (int)row["MaritalStatusID"],
                        WorkPlace = row["WorkPlace"].ToString(),
                        ImagePath = row["ImagePath"].ToString(),
                        ResidenceAddress = row["ResidenceAddress"].ToString(),
                        AdditionalData = row["AdditionalData"].ToString(),
                        Detentions = FromDataTableToDetentionList(DetentionTable, (int)row["DetaineeID"])

                    });
                }
                //conn.Open();
                //reader = command.ExecuteReader();

                //while (reader.Read())
                //{
                //    list.Add(new Detainee
                //    {
                //        DetaineeID = reader.GetInt32(0),
                //        FirstName = reader.GetString(1),
                //        LastName = reader.GetString(2),
                //        MiddleName = reader[3].ToString(),
                //        BirstDate = reader.GetDateTime(4),
                //        MaritalStatusID = reader.GetInt32(5),
                //        WorkPlace = reader.GetString(6),
                //        ImagePath = reader.GetString(7),
                //        ResidenceAddress = reader.GetString(8),
                //        AdditionalData = reader[9].ToString(),
                //        Detentions = GetDetentionsByDetaineeID(reader.GetInt32(0))

                //    });
                //}
                //reader.Close();

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

                if (adapter != null)
                {
                    adapter.Dispose();
                }
            }

            return list;
        }

        public IEnumerable<Detainee> GetDetaineesByDate(DateTime date)
        {
            List<Detainee> list = new List<Detainee>();

            SqlConnection conn = new SqlConnection(_connection);
            SqlCommand command = new SqlCommand("SelectDetaineesByDate", conn) { CommandType = CommandType.StoredProcedure };
            command.Parameters.AddWithValue("@Date",date);
            SqlDataReader reader = null;

            try
            {

                conn.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Detainee
                    {
                        DetaineeID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        MiddleName = reader[3].ToString(),
                        BirstDate = reader.GetDateTime(4),
                        MaritalStatusID = reader.GetInt32(5),
                        ImagePath = reader.GetString(6),
                        WorkPlace = reader.GetString(7),
                        ResidenceAddress = reader.GetString(8),
                        AdditionalData = reader[9].ToString(),

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

        public IEnumerable<MaritalStatus> GetAllMaritalStatusesFromTable()
        {
            List<MaritalStatus> list = new List<MaritalStatus>();

            SqlConnection conn = new SqlConnection(_connection);
            SqlCommand command = new SqlCommand("SelectAllMaritalStatuses", conn) { CommandType = CommandType.StoredProcedure };

            SqlDataReader reader = null;

            try
            {
                conn.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new MaritalStatus
                    {
                        StatusID = reader.GetInt32(0),
                        StatusName = reader.GetString(1),

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



        public void Create(Detainee dtn)
        {
            SqlConnection connection = new SqlConnection(_connection);
            SqlCommand command = new SqlCommand("CreateDetainee", connection) { CommandType = CommandType.StoredProcedure };

            try
            {
                connection.Open();

                SqlParameter[] parameters = {
                    new SqlParameter() { ParameterName = "@FirstName", Value = dtn.FirstName },
                    new SqlParameter() { ParameterName = "@LastName", Value = dtn.LastName },
                    new SqlParameter() { ParameterName = "@MiddleName", Value = dtn.MiddleName??"" },
                    new SqlParameter() { ParameterName = "@BirstDate", Value = dtn.BirstDate.ToShortDateString() },
                    new SqlParameter() { ParameterName = "@MaritalStatusID", Value = dtn.MaritalStatusID },
                    new SqlParameter() { ParameterName = "@WorkPlace", Value = dtn.WorkPlace },
                    new SqlParameter() { ParameterName = "@ImagePath", Value = dtn.ImagePath??"no photo" },
                    new SqlParameter() { ParameterName = "@ResidenceAddress", Value = dtn.ResidenceAddress },
                    new SqlParameter() { ParameterName = "@AdditionalData", Value = dtn.AdditionalData??"" }
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

        public void Update(Detainee dtn)
        {
            SqlConnection connection = new SqlConnection(_connection);

            SqlCommand command = new SqlCommand("UpdateDetainee", connection) { CommandType = CommandType.StoredProcedure };

            try
            {
                connection.Open();

                SqlParameter[] parameters = {
                    new SqlParameter() { ParameterName = "@ID", Value = dtn.DetaineeID },
                    new SqlParameter() { ParameterName = "@FirstName", Value = dtn.FirstName },
                    new SqlParameter() { ParameterName = "@LastName", Value = dtn.LastName },
                    new SqlParameter() { ParameterName = "@MiddleName", Value = dtn.MiddleName??"" },
                    new SqlParameter() { ParameterName = "@BirstDate", Value = dtn.BirstDate.ToShortDateString() },
                    new SqlParameter() { ParameterName = "@MaritalStatusID", Value = dtn.MaritalStatusID },
                    new SqlParameter() { ParameterName = "@WorkPlace", Value = dtn.WorkPlace },
                    new SqlParameter() { ParameterName = "@ImagePath", Value = dtn.ImagePath??"no photo" },
                    new SqlParameter() { ParameterName = "@ResidenceAddress", Value = dtn.ResidenceAddress },
                    new SqlParameter() { ParameterName = "@AdditionalData", Value = dtn.AdditionalData??"" }
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

            SqlCommand command = new SqlCommand("DeleteDetainee", connection) { CommandType = CommandType.StoredProcedure };

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

        private IEnumerable<Detention> FromDataTableToDetentionList(DataTable table,int id)
        {
            List <Detention> resultList= new List<Detention>();
            var rowCollection=table.AsEnumerable().Where(dr => dr.Field<int>("DetaineeID") == id);

            foreach (var row in rowCollection)
            {
                resultList.Add(
                    new Detention
                    {
                        DetentionID =row.Field<int>("DetentionID"),
                        DetentionDate = row.Field<DateTime>("DetentionDate"),
                        DetainedByWhomID = row.Field<int>("DetainedByWhomID"),
                        DeliveryDate = row.Field<DateTime>("DeliveryDate"),
                        DeliveredByWhomID = row.Field<int>("DeliveredByWhomID"),
                        ReleasеDate = row.Field<DateTime>("ReleasеDate"),
                        ReleasedByWhomID = row.Field<int>("ReleasedByWhomID"),
                        PlaceID = row.Field<int>("PlaceID"),
                        AmountForStaying = row.Field<decimal>("AmountForStaying"),
                        PaidAmount = row.Field<decimal>("PaidAmount"),
                    }
                    );
            }
            return resultList;
        }

    }
}