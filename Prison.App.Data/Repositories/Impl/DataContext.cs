using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Common.Loggers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prison.App.Data.Repositories.Impl
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

        public SqlParameter CreateParameter(string parameterName, object value, SqlDbType sqlDbType=SqlDbType.Variant,string typeName=null)
        {
            return new SqlParameter()
            {
                ParameterName = parameterName,
                Value = value,
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
                resultArray[index++]= CreateParameter(param.Key, param.Value);
            }
            return resultArray;
        }




    }
}
