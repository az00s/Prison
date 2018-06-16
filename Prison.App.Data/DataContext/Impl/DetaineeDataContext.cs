using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.DataContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Prison.App.Data.Repositories.Impl
{
    internal class DetaineeDataContext:IDetaineeDataContext
    {
        private IDataContext<Detainee> _context;

        public DetaineeDataContext(IDataContext<Detainee> context)
        {
            ArgumentHelper.ThrowExceptionIfNull(context, "IDataContext<Detainee>");

            _context = context;
        }

        public IReadOnlyCollection<Detainee> GetAllDetainees()
        {
            IReadOnlyCollection<Detainee> detaineeList = new List<Detainee>();

            var dataSet = _context.ExecuteQuery("SelectAllDetainees", null, CommandType.StoredProcedure);

            detaineeList = ToDetaineeList(dataSet);

            return detaineeList;
        }

        public IReadOnlyCollection<Detention> GetAllDetentions()
        {
            IReadOnlyCollection<Detention> detentionList = new List<Detention>();

            var dataSet = _context.ExecuteQuery("SelectAllDetentions", null, CommandType.StoredProcedure);

            detentionList = ToDetentionList(dataSet);

            return detentionList;
        }

        public Detainee GetDetaineeByID(int id)
        {
            Detainee detainee;

            IDictionary<string, object> parameters = new Dictionary<string, object> { { "@ID",id } };

            var dataSet = _context.ExecuteQuery("SelectDetaineeByID", parameters, CommandType.StoredProcedure);

            detainee = ToDetainee(dataSet);

            return detainee;

        }

        public IReadOnlyCollection<MaritalStatus> GetAllMaritalStatuses()
        {
            var dataSet = _context.ExecuteQuery("SelectAllMaritalStatuses", null, CommandType.StoredProcedure);

            var statusList = ToMaritalStatusList(dataSet);

            return statusList;
        }

        public IReadOnlyCollection<Detainee> GetDetaineesByDate(DateTime date)
        {
            IReadOnlyCollection<Detainee> detaineeList = new List<Detainee>();

            IDictionary<string, object> parameters = new Dictionary<string, object> { { "@Date", date } };

            var dataSet = _context.ExecuteQuery("SelectDetaineesByDate", parameters, CommandType.StoredProcedure);

            detaineeList = ToDetaineeList(dataSet);

            return detaineeList;
        }

        public IReadOnlyCollection<Detainee> Find(string DetentionDate = null, string FirstName = null, string LastName = null, string MiddleName = null, string ResidenceAddress = null)
        {
            var parameters = 
                new Dictionary<string, object>
                {
                    { "@FirstName", FirstName },
                    { "@LastName", LastName },
                    { "@MiddleName", MiddleName },
                    { "@DetentionDate", DetentionDate },
                    { "@Address", ResidenceAddress },
                };

            var dataSet = _context.ExecuteQuery("SelectDetaineeByParams", parameters, CommandType.StoredProcedure);

            var detaineeList = ToDetaineeList(dataSet);

            return detaineeList;
        }

        public void Create(Detainee dtn)
        {
            IDictionary<string, object> parametersDictionary =
                new Dictionary<string, object>
                {
                    { "@FirstName", dtn.FirstName },
                    { "@LastName", dtn.LastName },
                    { "@MiddleName", dtn.MiddleName??"" },
                    { "@BirstDate", dtn.BirstDate.ToShortDateString() },
                    { "@MaritalStatusID", dtn.MaritalStatusID },
                    { "@WorkPlace", dtn.WorkPlace },
                    { "@ImagePath", dtn.ImagePath??"/Content/Images/ProfilePhotos/noavatar.png" },
                    { "@ResidenceAddress", dtn.ResidenceAddress },
                    { "@AdditionalData", dtn.AdditionalData??"" }
                };

            //create complex parameter, which correlates with custom type in db "RoleIdTable"
            //stored procedure "CreateUser" needs this complex parameter
            var customParameter = _context.CreateCustomParameter("@DetentionTable", dtn.Detentions.ToList()[0], SqlDbType.Structured, "DetentionTable");
            var customParameter2 = _context.CreateCustomParameter("@PhoneTable", dtn.PhoneNumbers, "Number", SqlDbType.Structured, "PhoneNumberTable");

            //get all simpleparameters
            var parameters = _context.GetParameterList(parametersDictionary);

            //get all parameters
            parameters.Add(customParameter);
            parameters.Add(customParameter2);

            _context.ExecuteNonQuery("CreateDetainee", parameters.ToArray(), CommandType.StoredProcedure);
        }

        public void Update(Detainee dtn)
        {
            IDictionary<string, object> parametersDictionary =
                new Dictionary<string, object>
                {
                    { "@ID", dtn.DetaineeID },
                    { "@FirstName", dtn.FirstName },
                    { "@LastName", dtn.LastName },
                    { "@MiddleName", dtn.MiddleName??"" },
                    { "@BirstDate", dtn.BirstDate.ToShortDateString() },
                    { "@MaritalStatusID", dtn.MaritalStatusID },
                    { "@WorkPlace", dtn.WorkPlace },
                    { "@ImagePath",  dtn.ImagePath??"/Content/Images/ProfilePhotos/noavatar.png" },
                    { "@ResidenceAddress", dtn.ResidenceAddress },
                    { "@AdditionalData", dtn.AdditionalData??"" }
                };

            var customParameter = _context.CreateCustomParameter("@PhoneTable", dtn.PhoneNumbers, "Number", SqlDbType.Structured, "PhoneNumberTable");

            //get all simpleparameters
            var parameters = _context.GetParameterList(parametersDictionary);

            parameters.Add(customParameter);

            _context.ExecuteNonQuery("UpdateDetainee", parameters.ToArray(), CommandType.StoredProcedure);
        }

        public void ReleaseDetainee(Detention detention)
        {
            IDictionary<string, object> parametersDictionary =
                new Dictionary<string, object>
                {
                    { "@ID", detention.DetentionID },
                    { "@ReleaseDate", detention.ReleasеDate },
                    { "@ReleasedByWhomID", detention.ReleasedByWhomID },
                    { "@PaidAmount", detention.PaidAmount },
                    { "@AmountForStaying", detention.AmountForStaying },
                };

            _context.ExecuteNonQuery("ReleaseDetainee", parametersDictionary, CommandType.StoredProcedure);
        }

        public Detention GetLastDetention(int id)
        {
            IDictionary<string, object> parametersDictionary =
                new Dictionary<string, object>
                {
                    { "@ID", id }
                };

            var dataSet = _context.ExecuteQuery("SelectLastDetention", parametersDictionary, CommandType.StoredProcedure);
            var detention = ToDetention(dataSet);

            return detention;
        }

        public Detention GetDetentionByID(int id)
        {
            IDictionary<string, object> parametersDictionary =
                new Dictionary<string, object>
                {
                    { "@ID", id }
                };

            var dataSet = _context.ExecuteQuery("SelectDetentionByID", parametersDictionary, CommandType.StoredProcedure);
            var detention = ToDetention(dataSet);

            return detention;
        }

        public void Delete(int id)
        {
            IDictionary<string, object> parameters =
                new Dictionary<string, object>
                {
                    { "@ID", id },
                };

            _context.ExecuteNonQuery("DeleteDetainee", parameters, CommandType.StoredProcedure);
        }

        #region Converters
        private IReadOnlyCollection<Detainee> ToDetaineeList(DataSet dataset)
        {
            if (dataset.Tables.Count < 1)
            {
                return null;
            }

            var list = new List<Detainee>();
            var DetaineeTable = dataset.Tables[0];

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

                });
            }
            return list;
        }

        private IReadOnlyCollection<Detention> ToDetentionList(DataTable table, int id)
        {
            List<Detention> resultList = new List<Detention>();
            var rowCollection = table.AsEnumerable().Where(dr => dr.Field<int>("DetaineeID") == id);

            foreach (var row in rowCollection)
            {
                resultList.Add(
                    new Detention
                    {
                        DetentionID = row.Field<int>("DetentionID"),
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

        private IReadOnlyCollection<Detention> ToDetentionList(DataSet dataset)
        {
            var resultList = new List<Detention>();
            var rowCollection = dataset.Tables[0].AsEnumerable();

            foreach (var row in rowCollection)
            {
                resultList.Add(
                    new Detention
                    {
                        DetentionID = row.Field<int>("DetentionID"),
                        DetentionDate = row.Field<DateTime>("DetentionDate"),
                        DetainedByWhomID = row.Field<int>("DetainedByWhomID"),
                        DeliveryDate = row.Field<DateTime>("DeliveryDate"),
                        DeliveredByWhomID = row.Field<int>("DeliveredByWhomID"),
                        ReleasеDate = row.IsNull("ReleasеDate") ? DateTime.MinValue : row.Field<DateTime>("ReleasеDate"),
                        ReleasedByWhomID = row.IsNull("ReleasedByWhomID") ? 0 : row.Field<int>("ReleasedByWhomID"),
                        PlaceID = row.Field<int>("PlaceID"),
                        AmountForStaying = row.IsNull("AmountForStaying") ? 0 : row.Field<decimal>("AmountForStaying"),
                        PaidAmount = row.IsNull("PaidAmount") ? 0 : row.Field<decimal>("PaidAmount"),
                    }
                    );
            }
            return resultList;
        }

        private Detention ToDetention(DataSet dataset)
        {
            var row = dataset.Tables[0].Rows[0];

            return new Detention
            {
                DetentionID = row.Field<int>("DetentionID"),
                DetentionDate = row.Field<DateTime>("DetentionDate"),
                DetainedByWhomID = row.Field<int>("DetainedByWhomID"),
                DeliveryDate = row.Field<DateTime>("DeliveryDate"),
                DeliveredByWhomID = row.Field<int>("DeliveredByWhomID"),
                ReleasеDate = row.IsNull("ReleasеDate") ? DateTime.MinValue : row.Field<DateTime>("ReleasеDate"),
                ReleasedByWhomID = row.IsNull("ReleasedByWhomID") ? 0 : row.Field<int>("ReleasedByWhomID"),
                PlaceID = row.Field<int>("PlaceID"),
                AmountForStaying = row.IsNull("AmountForStaying") ? 0 : row.Field<decimal>("AmountForStaying"),
                PaidAmount = row.IsNull("PaidAmount") ? 0 : row.Field<decimal>("PaidAmount"),
            };
                   
            
             
        }

        private IReadOnlyCollection<Detention> ToDetentionList(DataTable table)
        {
            var resultList = new List<Detention>();
            var rowCollection = table.AsEnumerable();

            foreach (var row in rowCollection)
            {
                resultList.Add(
                    new Detention
                    {
                        DetentionID = row.Field<int>("DetentionID"),
                        DetentionDate = row.Field<DateTime>("DetentionDate"),
                        DetainedByWhomID = row.Field<int>("DetainedByWhomID"),
                        DeliveryDate = row.Field<DateTime>("DeliveryDate"),
                        DeliveredByWhomID = row.Field<int>("DeliveredByWhomID"),
                        ReleasеDate = row.IsNull("ReleasеDate")?DateTime.MinValue:row.Field<DateTime>("ReleasеDate"),
                        ReleasedByWhomID = row.IsNull("ReleasedByWhomID") ? 0 : row.Field<int>("ReleasedByWhomID"),
                        PlaceID = row.Field<int>("PlaceID"),
                        AmountForStaying = row.IsNull("AmountForStaying") ? 0 : row.Field<decimal>("AmountForStaying"),
                        PaidAmount = row.IsNull("PaidAmount") ? 0 : row.Field<decimal>("PaidAmount"),
                    }
                    );
            }
            return resultList;
        }

        private IReadOnlyCollection<string> ToPhoneNumberList(DataTable table)
        {
            var resultList = new List<string>();
            var rowCollection = table.AsEnumerable();

            foreach (var row in rowCollection)
            {
                resultList.Add(row.Field<string>(0) );
            }
            return resultList;
        }

        private Detainee ToDetainee(DataSet dataset)
        {
            var row = dataset.Tables[0].Rows[0];
            var DetentionTable = dataset.Tables[1];
            var phoneNumberTable = dataset.Tables[2];
            return new Detainee
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
                Detentions = ToDetentionList(DetentionTable),
                PhoneNumbers=ToPhoneNumberList(phoneNumberTable)

            };
        }

        private MaritalStatus ToMaritalStatus(DataSet dataset)
        {
            var row = dataset.Tables[0].Rows[0];
            return new MaritalStatus
            {
                StatusID = row.Field<int>(0),
                StatusName = row.Field<string>(1),

            };
        }

        private IReadOnlyCollection<MaritalStatus> ToMaritalStatusList(DataSet dataset)
        {
            var list = new List<MaritalStatus>();
            var statusTable = dataset.Tables[0];

            foreach (var row in statusTable.AsEnumerable())
            {
                list.Add(new MaritalStatus
                    {
                        StatusID = row.Field<int>(0),
                        StatusName = row.Field<string>(1),
                    });
            }
            return list;
        }
        #endregion




    }
}
