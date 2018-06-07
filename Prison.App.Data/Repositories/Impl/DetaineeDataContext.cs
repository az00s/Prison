using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IEnumerable<Detainee> GetAll()
        {
            List<Detainee> detaineeList = new List<Detainee>();

            var DataSet=_context.ExecuteQuery("SelectAllDetainees", null, CommandType.StoredProcedure);

            detaineeList = ToDetaineeList(DataSet);

            return detaineeList;
        }

        private List<Detainee> ToDetaineeList(DataSet dataset)
        {
            List<Detainee> list = new List<Detainee>();
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
                    Detentions = ToDetentionList(DetentionTable, (int)row["DetaineeID"])

                });
            }
            return list;
        }

        private IEnumerable<Detention> ToDetentionList(DataTable table, int id)
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


    }
}
