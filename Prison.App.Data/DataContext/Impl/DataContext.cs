using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Common.Loggers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Prison.App.Data.DataContext.Impl
{
    internal class DataContext<T>: IDataContext<T>
    {
        private string _connection;

        private ILogger _log;

        public DataContext(Logger log)
        {
            _connection = ConnectionStringHelper.GetConnectionString();
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");

            _log = log;
        }

        public DataSet ExecuteQuery(string cmdText,IDictionary<string,object> parameters, CommandType commandType)
        {
            SqlConnection connection=null;
            SqlCommand command=null;
            SqlDataAdapter adapter=null;
            DataSet dataset=null;

            try
            {
                connection = new SqlConnection(_connection);
                command = GetCommand(cmdText, connection, parameters, commandType);
                adapter = new SqlDataAdapter(command);
                dataset = new DataSet();

                if (adapter != null)
                {
                    connection.Open();
                    adapter.Fill(dataset);
                }
                connection.Close();
            }

            catch (InvalidOperationException ex)
            {
                _log.Error(ex.Message+$" \nConnection string:{connection.ConnectionString}, \nQueryText:{cmdText}");
            }

            catch (SqlException ex)
            {
                _log.Error(ex.Message + $" \nConnection string:{connection.ConnectionString}, \nQueryText:{cmdText}");
            }

            catch (InvalidCastException ex)
            {
                _log.Error(ex.Message + $" \nConnection string:{connection.ConnectionString}, \nQueryText:{cmdText}");
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }

                if (command != null)
                {
                    command.Dispose();
                }

                if (adapter != null)
                {
                    adapter.Dispose();
                }
            }

            return dataset;
        }

        public void ExecuteNonQuery(string cmdText, IDictionary<string, object> parameters, CommandType commandType)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            
            try
            {
                connection = new SqlConnection(_connection);
                command = GetCommand(cmdText, connection, parameters, commandType);

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();
            }

            catch (InvalidOperationException ex)
            {
                _log.Error(ex.Message + $" \nConnection string:{connection.ConnectionString}, \nQueryText:{cmdText}");
            }

            catch (SqlException ex)
            {
                _log.Error(ex.Message + $" \nConnection string:{connection.ConnectionString}, \nQueryText:{cmdText}");
            }

            catch (InvalidCastException ex)
            {
                _log.Error(ex.Message + $" \nConnection string:{connection.ConnectionString}, \nQueryText:{cmdText}");
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }

                if (command != null)
                {
                    command.Dispose();
                }

            }

        }

        public void ExecuteNonQuery(string cmdText, SqlParameter[] parameters, CommandType commandType)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connection);
                command = GetCommand(cmdText, connection, parameters, commandType);

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();
            }

            catch (InvalidOperationException ex)
            {
                _log.Error(ex.Message + $" \nConnection string:{connection.ConnectionString}, \nQueryText:{cmdText}");
            }

            catch (SqlException ex)
            {
                _log.Error(ex.Message + $" \nConnection string:{connection.ConnectionString}, \nQueryText:{cmdText}");
            }

            catch (InvalidCastException ex)
            {
                _log.Error(ex.Message + $" \nConnection string:{connection.ConnectionString}, \nQueryText:{cmdText}");
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }

                if (command != null)
                {
                    command.Dispose();
                }

            }

        }



        #region HelperMethods
        private SqlCommand GetCommand(string cmdText, SqlConnection connection, IDictionary<string, object> parameters, CommandType commandType)
        {
            SqlCommand command=new SqlCommand(cmdText, connection) { CommandType = commandType };

            if (parameters != null)
            {
                var sqlParameters = GetParameters(parameters);
                command.Parameters.AddRange(sqlParameters);
            }

            return command;
        }

        private SqlCommand GetCommand(string cmdText, SqlConnection connection, SqlParameter[] parameters, CommandType commandType)
        {
            SqlCommand command = new SqlCommand(cmdText, connection) { CommandType = commandType };
            command.Parameters.AddRange(parameters);
            return command;
        }

        public SqlParameter CreateCustomParameter(string parameterName, IEnumerable values, string columnName, SqlDbType sqlDbType=SqlDbType.Variant,string typeName=null)
        {
            return new SqlParameter()
            {
                ParameterName = parameterName,
                Value = CreateDataTable(values, columnName),
                SqlDbType = sqlDbType,
                TypeName = typeName
            };
        }

        public SqlParameter CreateCustomParameter(string parameterName, Detention value, SqlDbType sqlDbType = SqlDbType.Variant, string typeName = null)
        {
            return new SqlParameter()
            {
                ParameterName = parameterName,
                Value = CreateDataTable(value),
                SqlDbType = sqlDbType,
                TypeName = typeName
            };
        }


        private SqlParameter CreateParameter(string parameterName, object value)
        {
            return new SqlParameter()
            {
                ParameterName = parameterName,
                Value = value
            };
        }

        private SqlParameter[] GetParameters(IDictionary<string, object> parameters)
        {
            SqlParameter[] resultArray = new SqlParameter[parameters.Count];
            int index = 0;

            foreach (var param in parameters)
            {
                resultArray[index++] = CreateParameter(param.Key, param.Value);
            }
            return resultArray;
        }

        public List<SqlParameter> GetParameterList(IDictionary<string, object> parameters)
        {
            List<SqlParameter> resultList = new List<SqlParameter>();

            foreach (var param in parameters)
            {
                resultList.Add(CreateParameter(param.Key, param.Value));
            }
            return resultList;
        }


        private DataTable CreateDataTable(Detention detention)
        {
            DataTable table = new DataTable();
            table.Columns.Add("DetentionID", typeof(int));
            table.Columns.Add("DetentionDate", typeof(DateTime));
            table.Columns.Add("DetainedByWhomID", typeof(int));
            table.Columns.Add("DeliveryDate", typeof(DateTime));
            table.Columns.Add("DeliveredByWhomID", typeof(int));
            table.Columns.Add("PlaceID", typeof(int));

            table.Rows.Add(
                detention.DetentionID,
                detention.DetentionDate,
                detention.DetainedByWhomID,
                detention.DeliveryDate,
                detention.DeliveredByWhomID,
                detention.PlaceID);

            return table;
        }

        private DataTable CreateDataTable(IEnumerable values, string columnName)
        {
            DataTable table = new DataTable();
            table.Columns.Add(columnName);
            foreach (var val in values)
            {
                table.Rows.Add(val);
            }
            return table;
        }




        #endregion



    }
}
