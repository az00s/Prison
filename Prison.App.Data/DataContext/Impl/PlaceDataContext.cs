using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Prison.App.Data.DataContext.Impl
{
    internal class PlaceDataContext:IPlaceDataContext
    {
        private IDataContext<PlaceOfStay> _context;

        public PlaceDataContext(IDataContext<PlaceOfStay> context)
        {
            ArgumentHelper.ThrowExceptionIfNull(context, "IDataContext<PlaceOfStay>");

            _context = context;
        }

        public IEnumerable<PlaceOfStay> GetAllPlaces()
        {
            IEnumerable<PlaceOfStay> placeList = new List<PlaceOfStay>();

            var dataSet = _context.ExecuteQuery("SelectAllPlacesOfStay", null, CommandType.StoredProcedure);

            placeList = ToPlaceList(dataSet);

            return placeList;
        }

        public PlaceOfStay GetPlaceByID(int id)
        {
            PlaceOfStay place;

            IDictionary<string, object> parameters = new Dictionary<string, object> { { "@ID", id } };

            var dataSet = _context.ExecuteQuery("SelectPlaceOfStayByID", parameters, CommandType.StoredProcedure);

            place = ToPlace(dataSet);

            return place;

        }

        public void Create(PlaceOfStay dtn)
        {
            IDictionary<string, object> parameters =
                new Dictionary<string, object>
                {
                    { "@Address", dtn.Address },
                };

            _context.ExecuteNonQuery("CreatePlaceOfStay", parameters, CommandType.StoredProcedure);
        }

        public void Update(PlaceOfStay dtn)
        {
            IDictionary<string, object> parameters =
                 new Dictionary<string, object>
                 {
                    { "@ID", dtn.PlaceID },
                    { "@Address", dtn.Address },
                 };

            _context.ExecuteNonQuery("UpdatePlaceOfStay", parameters, CommandType.StoredProcedure);
        }

        public void Delete(int id)
        {
            IDictionary<string, object> parameters =
                new Dictionary<string, object>
                {
                    { "@ID", id },
                };

            _context.ExecuteNonQuery("DeletePlaceOfStay", parameters, CommandType.StoredProcedure);
        }



        #region Converters
        private IEnumerable<PlaceOfStay> ToPlaceList(DataSet dataset)
        {
            List<PlaceOfStay> list = new List<PlaceOfStay>();

            var placeTable = dataset.Tables[0];

            foreach (var row in placeTable.AsEnumerable())
            {
                list.Add(new PlaceOfStay
                {
                    PlaceID = row.Field<int>("PlaceID"),
                    Address = row.Field<string>("Address")
                });
            }
            return list;
        }

        private PlaceOfStay ToPlace(DataSet dataset)
        {
            var row = dataset.Tables[0].Rows[0];

            return new PlaceOfStay
            {
                PlaceID = row.Field<int>("PlaceID"),
                Address = row.Field<string>("Address"),

            };
        }

        #endregion

    }
}
