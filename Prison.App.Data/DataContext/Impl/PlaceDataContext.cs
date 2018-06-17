using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Prison.App.Data.DataContext.Impl
{
    internal class PlaceDataContext:IPlaceDataContext
    {
        private readonly IDataContext<PlaceOfStay> _context;

        public PlaceDataContext(IDataContext<PlaceOfStay> context)
        {
            ArgumentHelper.ThrowExceptionIfNull(context, "IDataContext<PlaceOfStay>");

            _context = context;
        }

        public IReadOnlyCollection<PlaceOfStay> GetAllPlaces()
        {
            var dataSet = _context.ExecuteQuery("SelectAllPlacesOfStay", null, CommandType.StoredProcedure);

            var placeList = ToPlaceList(dataSet);

            return placeList;
        }

        public PlaceOfStay GetPlaceByID(int id)
        {
            var parameters = new Dictionary<string, object> { { "@ID", id } };

            var dataSet = _context.ExecuteQuery("SelectPlaceOfStayByID", parameters, CommandType.StoredProcedure);

            var place = ToPlace(dataSet);

            return place;
        }

        public void Create(PlaceOfStay dtn)
        {
            var parameters =
                new Dictionary<string, object>
                {
                    { "@Address", dtn.Address },
                };

            _context.ExecuteNonQuery("CreatePlaceOfStay", parameters, CommandType.StoredProcedure);
        }

        public void Update(PlaceOfStay dtn)
        {
            var parameters =
                 new Dictionary<string, object>
                 {
                    { "@ID", dtn.PlaceID },
                    { "@Address", dtn.Address },
                 };

            _context.ExecuteNonQuery("UpdatePlaceOfStay", parameters, CommandType.StoredProcedure);
        }

        public void Delete(int id)
        {
            var parameters =
                new Dictionary<string, object>
                {
                    { "@ID", id },
                };

            _context.ExecuteNonQuery("DeletePlaceOfStay", parameters, CommandType.StoredProcedure);
        }

        #region Converters
        private IReadOnlyCollection<PlaceOfStay> ToPlaceList(DataSet dataset)
        {
            return dataset.Tables[0].AsEnumerable().Select(row=>
                new PlaceOfStay
                    {
                        PlaceID = row.Field<int>("PlaceID"),
                        Address = row.Field<string>("Address")
                    }
            ).ToList();

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
