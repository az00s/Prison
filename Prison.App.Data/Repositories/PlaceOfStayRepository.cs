using Prison.App.Data.Repositories.Common;
using Prison.App.Common.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Prison.App.Data.Repositories
{
    public class PlaceOfStayRepository: IDataCommonOperation<PlaceOfStay>
    {
        private string _connection;

        public PlaceOfStayRepository(string conn)
        {
            _connection = conn;
        }

        public IEnumerable<PlaceOfStay> GetAllRecordsFromTable()
        {
            List<PlaceOfStay> list = new List<PlaceOfStay>();

            using (SqlConnection conn = new SqlConnection(_connection))
            {
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM PlaceOfStay", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {

                            list.Add(new PlaceOfStay
                            {
                                PlaceID = reader.GetInt32(0),
                                Address = reader.GetString(1),
                                

                            });
                        }
                    }

                }
            }
            return list;
        }

        public void Create(PlaceOfStay emp)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                connection.Open();

                var command = new SqlCommand(
                    "Insert into PlaceOfStay ([Address]) values(@Address)"
                    , connection);

                SqlParameter[] parameters = {
                   
                    new SqlParameter() { ParameterName = "@Address", Value = emp.Address }
                    
                };


                command.Parameters.AddRange(parameters);

                command.ExecuteNonQuery();
            }
        }

        public void Update(PlaceOfStay emp)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("UPDATE PlaceOfStay SET [Address] = @Address WHERE PlaceID=@ID ", connection);

                SqlParameter[] parameters = {
                    new SqlParameter() { ParameterName = "@ID", Value = emp.PlaceID },
                    new SqlParameter() { ParameterName = "@Address", Value = emp.Address }
                   

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

                SqlCommand command = new SqlCommand("Delete from PlaceOfStay where PlaceID=@ID", connection);

                SqlParameter[] parameters = {
                    new SqlParameter() { ParameterName = "@ID", Value = id }
                };

                command.Parameters.AddRange(parameters);

                command.ExecuteNonQuery();
            }
        }
    }
}
