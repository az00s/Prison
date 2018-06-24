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
        private readonly IDataContext<Detainee> _context;

        public DetaineeDataContext(IDataContext<Detainee> context)
        {
            ArgumentHelper.ThrowExceptionIfNull(context, "IDataContext<Detainee>");

            _context = context;
        }

        public IReadOnlyCollection<Detainee> GetAllDetainees()
        {
            var dataSet = _context.ExecuteQuery("SelectAllDetainees", null, CommandType.StoredProcedure);

            var detaineeList = ToDetaineeList(dataSet);

            return detaineeList;
        }

        public IReadOnlyCollection<Detention> GetAllDetentions()
        {
            var dataSet = _context.ExecuteQuery("SelectAllDetentions", null, CommandType.StoredProcedure);

            var detentionList = ToDetentionList(dataSet);

            return detentionList;
        }

        public IReadOnlyCollection<Detention> GetDetentionsForLast3Days()
        {
            var dataSet = _context.ExecuteQuery("SelectDetentionsForLast3Days", null, CommandType.StoredProcedure);

            var detentionList = ToDetentionList(dataSet);

            return detentionList;
        }

        public Detainee GetDetaineeByID(int id)
        {
            var parameters = new Dictionary<string, object> { { "@ID",id } };

            var dataSet = _context.ExecuteQuery("SelectDetaineeByID", parameters, CommandType.StoredProcedure);

            var detainee = ToDetainee(dataSet);

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
            var parameters = new Dictionary<string, object> { { "@Date", date } };

            var dataSet = _context.ExecuteQuery("SelectDetaineesByDate", parameters, CommandType.StoredProcedure);

            var detaineeList = ToDetaineeList(dataSet);

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
            var parametersDictionary =
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
            var parametersDictionary =
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

        public void ReleaseDetainee(Release release)
        {
            var parametersDictionary =
                new Dictionary<string, object>
                {
                    { "@DetaineeID", release.DetaineeID },
                    { "@DetentionID", release.DetentionID },
                    { "@ReleaseDate", release.ReleasеDate },
                    { "@ReleasedByWhomID", release.ReleasedByWhomID },
                    { "@PaidAmount", release.PaidAmount },
                    { "@AmountForStaying", release.AmountForStaying },
                };

            _context.ExecuteNonQuery("ReleaseDetainee", parametersDictionary, CommandType.StoredProcedure);
        }

        public Detention GetLastDetention(int id)
        {
            var parametersDictionary =
                new Dictionary<string, object>
                {
                    { "@ID", id }
                };

            var dataSet = _context.ExecuteQuery("SelectLastDetention", parametersDictionary, CommandType.StoredProcedure);
            var detention = ToDetention(dataSet);

            return detention;
        }

        public Release GetLastRelease(int id)
        {
            var parametersDictionary =
                new Dictionary<string, object>
                {
                    { "@ID", id }
                };

            var dataSet = _context.ExecuteQuery("SelectLastRelease", parametersDictionary, CommandType.StoredProcedure);
            var release = ToRelease(dataSet);

            return release;
        }


        public Detention GetDetentionByID(int id)
        {
            var parametersDictionary =
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
            var parameters =
                new Dictionary<string, object>
                {
                    { "@ID", id },
                };

            _context.ExecuteNonQuery("DeleteDetainee", parameters, CommandType.StoredProcedure);
        }

        #region Converters
        private IReadOnlyCollection<Detainee> ToDetaineeList(DataSet dataset)
        {
            return dataset.Tables[0].AsEnumerable().Select(row=>
                new Detainee
                {
                    DetaineeID = row.Field<int>("DetaineeID"),
                    FirstName = row.Field<string>("FirstName"),
                    LastName = row.Field<string>("LastName"),
                    MiddleName = row.Field<string>("MiddleName"),
                    BirstDate = row.Field<DateTime>("BirstDate"),
                    MaritalStatusID = row.Field<int>("MaritalStatusID"),
                    WorkPlace = row.Field<string>("WorkPlace"),
                    ImagePath = row.Field<string>("ImagePath"),
                    ResidenceAddress = row.Field<string>("ResidenceAddress"),
                    AdditionalData = row.Field<string>("AdditionalData"),
                })
                .ToList();
        }

        private IReadOnlyCollection<Detention> ToDetentionList(DataTable table, int id)
        {
            return table.AsEnumerable()
                .Where(dr => dr.Field<int>("DetaineeID") == id)
                .Select(row=>
                    new Detention
                        {
                            DetentionID = row.Field<int>("DetentionID"),
                            DetentionDate = row.Field<DateTime>("DetentionDate"),
                            DetainedByWhomID = row.Field<int>("DetainedByWhomID"),
                            DeliveryDate = row.Field<DateTime>("DeliveryDate"),
                            DeliveredByWhomID = row.Field<int>("DeliveredByWhomID"),
                            PlaceID = row.Field<int>("PlaceID"),
                        }
                ).ToList();
        }

        private IReadOnlyCollection<Detention> ToDetentionList(DataSet dataset)
        {
            return dataset.Tables[0]
                .AsEnumerable()
                .Select(row=>
                    new Detention
                    {
                        DetentionID = row.Field<int>("DetentionID"),
                        DetentionDate = row.Field<DateTime>("DetentionDate"),
                        DetainedByWhomID = row.Field<int>("DetainedByWhomID"),
                        DeliveryDate = row.Field<DateTime>("DeliveryDate"),
                        DeliveredByWhomID = row.Field<int>("DeliveredByWhomID"),
                        PlaceID = row.Field<int>("PlaceID"),
                    }
                ).ToList();
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
                PlaceID = row.Field<int>("PlaceID"),
            };
        }

        private Release ToRelease(DataSet dataset)
        {
            if (dataset.Tables[0].Rows.Count<1) return null;

            var row = dataset.Tables[0].Rows[0];

            return new Release
            {
                DetaineeID = row.Field<int>("DetaineeID"),
                DetentionID = row.Field<int>("DetentionID"),
                ReleasеDate = row.IsNull("ReleasеDate") ? DateTime.MinValue : row.Field<DateTime>("ReleasеDate"),
                ReleasedByWhomID = row.IsNull("ReleasedByWhomID") ? 0 : row.Field<int>("ReleasedByWhomID"),
                AmountForStaying = row.IsNull("AmountForStaying") ? 0 : row.Field<decimal>("AmountForStaying"),
                PaidAmount = row.IsNull("PaidAmount") ? 0 : row.Field<decimal>("PaidAmount"),
            };
        }


        private IReadOnlyCollection<Detention> ToDetentionList(DataTable table)
        {
            return table.AsEnumerable().Select(row=>
                new Detention
                {
                    DetentionID = row.Field<int>("DetentionID"),
                    DetentionDate = row.Field<DateTime>("DetentionDate"),
                    DetainedByWhomID = row.Field<int>("DetainedByWhomID"),
                    DeliveryDate = row.Field<DateTime>("DeliveryDate"),
                    DeliveredByWhomID = row.Field<int>("DeliveredByWhomID"),
                    PlaceID = row.Field<int>("PlaceID"),
                }

            ).ToList();
        }

        private IReadOnlyCollection<string> ToPhoneNumberList(DataTable table)
        {
            return table.AsEnumerable().Select(row=>row.Field<string>(0)).ToList();
        }

        private Detainee ToDetainee(DataSet dataset)
        {
            var row = dataset.Tables[0].Rows[0];
            var DetentionTable = dataset.Tables[1];
            var phoneNumberTable = dataset.Tables[2];

            return new Detainee
            {
                DetaineeID = row.Field<int>("DetaineeID"),
                FirstName = row.Field<string>("FirstName"),
                LastName = row.Field<string>("LastName"),
                MiddleName = row.Field<string>("MiddleName"),
                BirstDate = row.Field<DateTime>("BirstDate"),
                MaritalStatusID = row.Field<int>("MaritalStatusID"),
                WorkPlace = row.Field<string>("WorkPlace"),
                ImagePath = row.Field<string>("ImagePath"),
                ResidenceAddress = row.Field<string>("ResidenceAddress"),
                AdditionalData = row.Field<string>("AdditionalData"),
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
            return dataset.Tables[0].AsEnumerable().Select(row=>
                new MaritalStatus
                {
                    StatusID = row.Field<int>(0),
                    StatusName = row.Field<string>(1),
                }
            ).ToList();
        }
        #endregion




    }
}
