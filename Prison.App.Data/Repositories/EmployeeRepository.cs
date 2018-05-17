using Prison.App.Common.Entities;
using Prison.App.Data.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace Prison.App.Data.Repositories
{
    public class EmployeeRepository: IDataCommonOperation<Employee>
    {
        private string _connection;

        public EmployeeRepository(string conn)
        {
            _connection = conn;
        }

        public IEnumerable<Employee> GetAllRecordsFromTable()
        {
            List<Employee> list = new List<Employee>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connection))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("SELECT * FROM Employee", conn);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {

                                list.Add(new Employee
                                {
                                    EmployeeID = reader.GetInt32(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    Patronymic = reader[3].ToString(),
                                    PositionID = reader.GetInt32(4),
                                    Details = reader[5].ToString(),

                                });
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            //catch (InvalidOperationException ex)    
            //{

            //}
            //catch (SqlException ex) 
            //{

            //}
            //catch (ConfigurationErrorsException ex) 
            //{

            //}
            //catch (InvalidCastException ex) 
            //{

            //}

            //catch (IOException ex)
            //{
                
            //}

            


            return list;
        }

        public void Create(Employee emp)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                connection.Open();

                var command = new SqlCommand(
                    "Insert into Employee " +
                            "([FirstName],[LastName],[Patronymic],[PositionID],[Details])" +
                    " values(@FirstName,@LastName,@Patronymic,@PositionID,@Details)", connection);

                SqlParameter[] parameters = {
                    new SqlParameter() { ParameterName = "@FirstName", Value = emp.FirstName },
                    new SqlParameter() { ParameterName = "@LastName", Value = emp.LastName },
                    new SqlParameter() { ParameterName = "@Patronymic", Value = emp.Patronymic },
                    new SqlParameter() { ParameterName = "@PositionID", Value = emp.PositionID },
                    new SqlParameter() { ParameterName = "@Details", Value = emp.Details },
                };
                    

                command.Parameters.AddRange(parameters);

                command.ExecuteNonQuery();
            }
        }

        public void Update(Employee emp)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("UPDATE Employee " +
                    "SET [FirstName] = @FirstName," +
                    "[LastName] =@LastName," +
                    "[Patronymic] =@Patronymic," +
                    "[PositionID] = @PositionID," +
                    "[Details] = @Details " +
                    "WHERE EmployeeID=@ID ", connection);

                SqlParameter[] parameters = {
                    new SqlParameter() { ParameterName = "@ID", Value = emp.EmployeeID },
                    new SqlParameter() { ParameterName = "@FirstName", Value = emp.FirstName },
                    new SqlParameter() { ParameterName = "@LastName", Value = emp.LastName },
                    new SqlParameter() { ParameterName = "@Patronymic", Value = emp.Patronymic??"" },
                    new SqlParameter() { ParameterName = "@PositionID", Value = emp.PositionID },
                    new SqlParameter() { ParameterName = "@Details", Value = emp.Details },
                   
                };

                command.Parameters.AddRange(parameters);

                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("Delete from Employee where EmployeeID=@ID", connection);

                SqlParameter[] parameters = {
                    new SqlParameter() { ParameterName = "@ID", Value = id }
                };

                command.Parameters.AddRange(parameters);

                command.ExecuteNonQuery();
            }
        }
    }
}
