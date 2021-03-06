﻿using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Prison.App.Data.DataContext.Impl
{
    internal class MaritalStatusDataContext:IMaritalStatusDataContext
    {
        private readonly IDataContext<MaritalStatus> _context;

        public MaritalStatusDataContext(IDataContext<MaritalStatus> context)
        {
            ArgumentHelper.ThrowExceptionIfNull(context, "IDataContext<MaritalStatus>");

            _context = context;
        }

        public IReadOnlyCollection<MaritalStatus> GetAllStatuses()
        {
            var dataSet = _context.ExecuteQuery("SelectAllStatuses", null, CommandType.StoredProcedure);

            var statusList = ToStatusList(dataSet);

            return statusList;
        }

        public MaritalStatus GetStatusByID(int id)
        {
            var parameters = new Dictionary<string, object> { { "@ID", id } };

            var dataSet = _context.ExecuteQuery("SelectStatusByID", parameters, CommandType.StoredProcedure);

            var status = ToStatus(dataSet);

            return status;
        }

        public void Create(MaritalStatus dtn)
        {
            var parameters =new Dictionary<string, object>
                {
                    { "@StatusName", dtn.StatusName },
                };

            _context.ExecuteNonQuery("CreateStatus", parameters, CommandType.StoredProcedure);
        }

        public void Update(MaritalStatus dtn)
        {
            var parameters =new Dictionary<string, object>
                 {
                    { "@ID", dtn.StatusID },
                    { "@StatusName", dtn.StatusName },
                 };

            _context.ExecuteNonQuery("UpdateStatus", parameters, CommandType.StoredProcedure);
        }

        public void Delete(int id)
        {
            var parameters =
                new Dictionary<string, object>
                {
                    { "@ID", id },
                };

            _context.ExecuteNonQuery("DeleteStatus", parameters, CommandType.StoredProcedure);
        }

        #region Converters
        private IReadOnlyCollection<MaritalStatus> ToStatusList(DataSet dataset)
        {
            return dataset.Tables[0].AsEnumerable().Select(row=>
                new MaritalStatus
                {
                    StatusID = row.Field<int>("StatusID"),
                    StatusName = row.Field<string>("StatusName")
                }
            ).ToList();

        }

        private MaritalStatus ToStatus(DataSet dataset)
        {
            var row = dataset.Tables[0].Rows[0];

            return new MaritalStatus
            {
                StatusID = row.Field<int>("StatusID"),
                StatusName = row.Field<string>("StatusName"),

            };
        }

        #endregion
    }
}
