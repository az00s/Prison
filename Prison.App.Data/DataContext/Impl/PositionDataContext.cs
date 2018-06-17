using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Prison.App.Data.DataContext.Impl
{
    internal class PositionDataContext:IPositionDataContext
    {
        private readonly IDataContext<Position> _context;

        public PositionDataContext(IDataContext<Position> context)
        {
            ArgumentHelper.ThrowExceptionIfNull(context, "IDataContext<Position>");

            _context = context;
        }

        public IReadOnlyCollection<Position> GetAllPositions()
        {
            var dataSet = _context.ExecuteQuery("SelectAllPositions", null, CommandType.StoredProcedure);

            var positionList = ToPositionList(dataSet);

            return positionList;
        }

        public Position GetPositionByID(int id)
        {
            var parameters = new Dictionary<string, object> { { "@ID", id } };

            var dataSet = _context.ExecuteQuery("SelectPositionByID", parameters, CommandType.StoredProcedure);

            var position = ToPosition(dataSet);

            return position;
        }

        public void Create(Position dtn)
        {
            var parameters =
                new Dictionary<string, object>
                {
                    { "@PositionName", dtn.PositionName },
                };

            _context.ExecuteNonQuery("CreatePosition", parameters, CommandType.StoredProcedure);
        }

        public void Update(Position dtn)
        {
            var parameters =
                 new Dictionary<string, object>
                 {
                    { "@ID", dtn.PositionID },
                    { "@PositionName", dtn.PositionName },
                 };

            _context.ExecuteNonQuery("UpdatePosition", parameters, CommandType.StoredProcedure);
        }

        public void Delete(int id)
        {
            var parameters =
                new Dictionary<string, object>
                {
                    { "@ID", id },
                };

            _context.ExecuteNonQuery("DeletePosition", parameters, CommandType.StoredProcedure);
        }

        #region Converters
        private IReadOnlyCollection<Position> ToPositionList(DataSet dataset)
        {
            return dataset.Tables[0].AsEnumerable().Select(row=>
                new Position
                {
                    PositionID = row.Field<int>("PositionID"),
                    PositionName = row.Field<string>("PositionName")
                }
            ).ToList();
        }

        private Position ToPosition(DataSet dataset)
        {
            var row = dataset.Tables[0].Rows[0];

            return new Position
            {
                PositionID = row.Field<int>("PositionID"),
                PositionName = row.Field<string>("PositionName"),

            };
        }

        #endregion
    }
}
