using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Prison.App.Data.DataContext.Impl
{
    internal class PositionDataContext:IPositionDataContext
    {
        private IDataContext<Position> _context;

        public PositionDataContext(IDataContext<Position> context)
        {
            ArgumentHelper.ThrowExceptionIfNull(context, "IDataContext<Position>");

            _context = context;
        }

        public IEnumerable<Position> GetAllPositions()
        {
            IEnumerable<Position> positionList = new List<Position>();

            var dataSet = _context.ExecuteQuery("SelectAllPositions", null, CommandType.StoredProcedure);

            positionList = ToPositionList(dataSet);

            return positionList;
        }

        public Position GetPositionByID(int id)
        {
            Position position;

            IDictionary<string, object> parameters = new Dictionary<string, object> { { "@ID", id } };

            var dataSet = _context.ExecuteQuery("SelectPositionByID", parameters, CommandType.StoredProcedure);

            position = ToPosition(dataSet);

            return position;

        }

        public void Create(Position dtn)
        {
            IDictionary<string, object> parameters =
                new Dictionary<string, object>
                {
                    { "@PositionName", dtn.PositionName },
                };

            _context.ExecuteNonQuery("CreatePosition", parameters, CommandType.StoredProcedure);
        }

        public void Update(Position dtn)
        {
            IDictionary<string, object> parameters =
                 new Dictionary<string, object>
                 {
                    { "@ID", dtn.PositionID },
                    { "@PositionName", dtn.PositionName },
                 };

            _context.ExecuteNonQuery("UpdatePosition", parameters, CommandType.StoredProcedure);
        }

        public void Delete(int id)
        {
            IDictionary<string, object> parameters =
                new Dictionary<string, object>
                {
                    { "@ID", id },
                };

            _context.ExecuteNonQuery("DeletePosition", parameters, CommandType.StoredProcedure);
        }



        #region Converters
        private IEnumerable<Position> ToPositionList(DataSet dataset)
        {
            List<Position> list = new List<Position>();

            var positionTable = dataset.Tables[0];

            foreach (var row in positionTable.AsEnumerable())
            {
                list.Add(new Position
                {
                    PositionID = row.Field<int>("PositionID"),
                    PositionName = row.Field<string>("PositionName")
                });
            }
            return list;
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
