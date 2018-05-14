using Prison.App.Common.Entities;
using Prison.App.Data.Repositories.Common;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Prison.App.Data.Repositories
{
    public class DetaineeRepository: IDataCommonOperation<Detainee>
    {
        private string _connection;

        public DetaineeRepository(string conn)
        {
            _connection = conn;
        }

        IEnumerable<Detention> GetDetentionsByDetaineeID(int id)
        {
            List<Detention> list = new List<Detention>();

            using (SqlConnection conn = new SqlConnection(_connection))
            {
                conn.Open();

                SqlCommand AllDetentionsCommand = new SqlCommand("select Detention.[DetentionID],[DetentionDate],[DetainedByWhomID],[DeliveryDate],[DeliveredByWhomID]," +
                    "[ReleasеDate],[ReleasedByWhomID],[PlaceOfStayID],[AmountForStaying],[PaidAmount]" +
                    " from DetentionsOfDetainees inner join Detention" +
                    " on Detention.DetentionID = DetentionsOfDetainees.DetentionID " +
                    "where DetentionsOfDetainees.DetaineeID = @ID", conn);

                AllDetentionsCommand.Parameters.Add(new SqlParameter() { ParameterName="@ID",Value=id});

                using (SqlDataReader reader = AllDetentionsCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {

                            list.Add(new Detention
                            {
                                DetentionID = reader.GetInt32(0),
                                DetentionDate=reader.GetDateTime(1),
                                DetainedByWhomID = reader.GetInt32(2),
                                DeliveryDate = reader.GetDateTime(3),
                                DeliveredByWhomID = reader.GetInt32(4),
                                ReleasеDate = reader.GetDateTime(5),
                                ReleasedByWhomID = reader.GetInt32(6),
                                PlaceOfStayID = reader.GetInt32(7),
                                AmountForStaying = reader.GetDecimal(8),
                                PaidAmount = reader.GetDecimal(9),
                            });
                        }
                    }

                }
            }

            return list;
        }

        public IEnumerable<Detainee> GetAllRecordsFromTable()
        {
            List<Detainee> list = new List<Detainee>();

            using (SqlConnection conn = new SqlConnection(_connection))
            {
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Detainee", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {

                            list.Add(new Detainee
                            {
                                DetaineeID = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Patronymic = reader[3].ToString(),
                                BirstDate = reader.GetDateTime(4),
                                MaritalStatusID = reader.GetInt32(5),
                                WorkPlace = reader.GetString(6),
                                Photo = reader.GetString(7),
                                ResidenceAddress = reader.GetString(8),
                                AdditionalData = reader[9].ToString(),
                                Detentions = GetDetentionsByDetaineeID(reader.GetInt32(0))

                            });
                        }
                    }

                }
            }
            return list;
        }

        public void Create(Detainee dtn)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                connection.Open();

                var command = new SqlCommand(
                    "Insert into Detainee " +
                            "([FirstName],[LastName],[Patronymic],[BirstDate],[MaritalStatusID],[WorkPlace],[Photo],[ResidenceAddress],[AdditionalData])" +
                    " values(@FirstName,@LastName,@Patronymic,@BirstDate,@MaritalStatusID,@WorkPlace,@Photo,@ResidenceAddress,@AdditionalData)", connection);

                SqlParameter[] parameters = {
                    new SqlParameter() { ParameterName = "@FirstName", Value = dtn.FirstName },
                    new SqlParameter() { ParameterName = "@LastName", Value = dtn.LastName },
                    new SqlParameter() { ParameterName = "@Patronymic", Value = dtn.Patronymic },
                    new SqlParameter() { ParameterName = "@BirstDate", Value = dtn.BirstDate.ToShortDateString() },
                    new SqlParameter() { ParameterName = "@MaritalStatusID", Value = dtn.MaritalStatusID },
                    new SqlParameter() { ParameterName = "@WorkPlace", Value = dtn.WorkPlace },
                    new SqlParameter() { ParameterName = "@Photo", Value = dtn.Photo },
                    new SqlParameter() { ParameterName = "@ResidenceAddress", Value = dtn.ResidenceAddress },
                    new SqlParameter() { ParameterName = "@AdditionalData", Value = dtn.AdditionalData }
                };

                command.Parameters.AddRange(parameters);

                command.ExecuteNonQuery();
            }
        }

        public void Update(Detainee dtn)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("UPDATE Detainee " +
                    "SET [FirstName] = @FirstName," +
                    "[LastName] =@LastName," +
                    "[Patronymic] =@Patronymic," +
                    "[BirstDate] = @BirstDate," +
                    "[MaritalStatusID] = @MaritalStatusID," +
                    "[WorkPlace] =@WorkPlace, " +
                    "[Photo] = @Photo," +
                    "[ResidenceAddress] = @ResidenceAddress," +
                    "[AdditionalData] = @AdditionalData " +
                    "WHERE DetaineeID =@ID ", connection);

                SqlParameter[] parameters = {
                    new SqlParameter() { ParameterName = "@ID", Value = dtn.DetaineeID },
                    new SqlParameter() { ParameterName = "@FirstName", Value = dtn.FirstName },
                    new SqlParameter() { ParameterName = "@LastName", Value = dtn.LastName },
                    new SqlParameter() { ParameterName = "@Patronymic", Value = dtn.Patronymic },
                    new SqlParameter() { ParameterName = "@BirstDate", Value = dtn.BirstDate.ToShortDateString() },
                    new SqlParameter() { ParameterName = "@MaritalStatusID", Value = dtn.MaritalStatusID },
                    new SqlParameter() { ParameterName = "@WorkPlace", Value = dtn.WorkPlace },
                    new SqlParameter() { ParameterName = "@Photo", Value = dtn.Photo },
                    new SqlParameter() { ParameterName = "@ResidenceAddress", Value = dtn.ResidenceAddress },
                    new SqlParameter() { ParameterName = "@AdditionalData", Value = dtn.AdditionalData }
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

                SqlCommand command = new SqlCommand("Delete from Detainee where DetaineeID=@ID", connection);

                SqlParameter[] parameters = {
                    new SqlParameter() { ParameterName = "@ID", Value = id }
                };

                command.Parameters.AddRange(parameters);

                command.ExecuteNonQuery();
            }
        }

    }
}
