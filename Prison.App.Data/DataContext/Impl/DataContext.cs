using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Prison.App.Data.DataContext.Impl
{
    internal class DataContext<T>: IDataContext<T>
    {
        private string _connection;

        public DataContext()
        {
            _connection = ConnectionStringHelper.GetConnectionString();
        }

        public DataSet ExecuteQuery(string cmdText,IDictionary<string,object> parameters, CommandType commandType)
        {
            using (IDbConnection connection = new SqlConnection(_connection))
            using (IDbCommand command= GetCommand(cmdText, connection, parameters, commandType))
            using (DbDataAdapter adapter = new SqlDataAdapter(command as SqlCommand))
            {   
                var dataset = new DataSet();
                adapter.Fill(dataset);
                return dataset;
            }
        }

        public void ExecuteNonQuery(string cmdText, IDictionary<string, object> parameters, CommandType commandType)
        {
            using (IDbConnection connection = new SqlConnection(_connection))
            using (IDbCommand command = GetCommand(cmdText, connection, parameters, commandType))
            using (DbDataAdapter adapter = new SqlDataAdapter(command as SqlCommand))
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void ExecuteNonQuery(string cmdText, IDataParameter[] parameters, CommandType commandType)
        {
            using (IDbConnection connection = new SqlConnection(_connection))
            using (IDbCommand command = GetCommand(cmdText, connection, parameters, commandType))
            using (DbDataAdapter adapter = new SqlDataAdapter(command as SqlCommand))
            {
                connection.Open();
                command.ExecuteNonQuery();
            }

        }

        #region HelperMethods
        private IDbCommand GetCommand(string cmdText, IDbConnection connection, IDictionary<string, object> parameters, CommandType commandType)
        {
            SqlCommand command=new SqlCommand(cmdText, connection as SqlConnection) { CommandType = commandType };

            if (parameters != null)
            {
                var sqlParameters = GetParameters(parameters);
                command.Parameters.AddRange(sqlParameters);
            }

            return command;
        }

        private IDbCommand GetCommand(string cmdText, IDbConnection connection, IDataParameter[] parameters, CommandType commandType)
        {
            IDbCommand command = new SqlCommand(cmdText, connection as SqlConnection) { CommandType = commandType };
            foreach (var item in parameters)
            {
                command.Parameters.Add(item);
            }
            return command;
        }

        public IDataParameter CreateCustomParameter(string parameterName, IEnumerable values, string columnName, SqlDbType sqlDbType=SqlDbType.Variant,string typeName=null)
        {
            return new SqlParameter()
            {
                ParameterName = parameterName,
                Value = CreateDataTable(values, columnName),
                SqlDbType = sqlDbType,
                TypeName = typeName
            };
        }

        public IDataParameter CreateCustomParameter(string parameterName, Detention value, SqlDbType sqlDbType = SqlDbType.Variant, string typeName = null)
        {
            return new SqlParameter()
            {
                ParameterName = parameterName,
                Value = CreateDataTable(value),
                SqlDbType = sqlDbType,
                TypeName = typeName
            };
        }

        private IDataParameter CreateParameter(string parameterName, object value)
        {
            return new SqlParameter()
            {
                ParameterName = parameterName,
                Value = value
            };
        }

        private IDataParameter[] GetParameters(IDictionary<string, object> parameters)
        {
            IDataParameter[] resultArray = new SqlParameter[parameters.Count];
            int index = 0;

            foreach (var param in parameters)
            {
                resultArray[index++] = CreateParameter(param.Key, param.Value);
            }
            return resultArray;
        }

        public List<IDataParameter> GetParameterList(IDictionary<string, object> parameters)
        {
            List<IDataParameter> resultList = new List<IDataParameter>();

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
