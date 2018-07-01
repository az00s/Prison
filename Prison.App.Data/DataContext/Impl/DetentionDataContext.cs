using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Prison.App.Data.DataContext.Impl
{
    internal class DetentionDataContext: IDetentionDataContext
    {
        private readonly IDataContext<Detention> _context;

        public DetentionDataContext(IDataContext<Detention> context)
        {
            ArgumentHelper.ThrowExceptionIfNull(context, "IDataContext<Detention>");

            _context = context;
        }

        public IReadOnlyCollection<Detention> GetAll()
        {
            var dataSet = _context.ExecuteQuery("SelectAllDetentions", null, CommandType.StoredProcedure);

            var detentionList = ToDetentionList(dataSet);

            return detentionList;
        }

        public Detention GetByID(int id)
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

        public IReadOnlyCollection<Detention> GetDetentionsForLast3Days()
        {
            var dataSet = _context.ExecuteQuery("SelectDetentionsForLast3Days", null, CommandType.StoredProcedure);

            var detentionList = ToDetentionList(dataSet);

            return detentionList;
        }

        public void Create(Detention dtn)
        {
            var parametersDictionary =
                new Dictionary<string, object>
                {
                    { "@DetaineeID", dtn.DetentionID },
                    { "@DetentionDate", dtn.DetentionDate },
                    { "@DetainedByWhomID", dtn.DetainedByWhomID },
                    { "@DeliveryDate", dtn.DeliveryDate },
                    { "@DeliveredByWhomID", dtn.DeliveredByWhomID },
                    { "@PlaceID", dtn.PlaceID }
                };

            _context.ExecuteNonQuery("CreateDetention", parametersDictionary, CommandType.StoredProcedure);
        }

        public Detention GetLast(int id)
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


        #region Helpers

        private IReadOnlyCollection<Detention> ToDetentionList(DataTable table, int id)
        {
            return table.AsEnumerable()
                .Where(dr => dr.Field<int>("DetaineeID") == id)
                .Select(row =>
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
                .Select(row =>
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

        #endregion
    }
}
