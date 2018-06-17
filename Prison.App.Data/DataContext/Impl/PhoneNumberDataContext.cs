using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Prison.App.Data.DataContext.Impl
{
    internal class PhoneNumberDataContext:IPhoneNumberDataContext
    {
        private readonly IDataContext<PhoneNumber> _context;

        public PhoneNumberDataContext(IDataContext<PhoneNumber> context)
        {
            ArgumentHelper.ThrowExceptionIfNull(context, "IDataContext<PhoneNumber>");

            _context = context;
        }

        public IReadOnlyCollection<PhoneNumber> GetAllNumbers()
        {
            var dataSet = _context.ExecuteQuery("SelectAllNumbers", null, CommandType.StoredProcedure);

            var numberList = ToNumberList(dataSet);

            return numberList;
        }

        public IReadOnlyCollection<Detainee> GetAllDetaineeLastNames()
        {
            var dataSet = _context.ExecuteQuery("SelectAllDetaineeLastNames", null, CommandType.StoredProcedure);

            var numberList = ToDetaineeList(dataSet);

            return numberList;
        }

        public PhoneNumber GetNumberByID(int id)
        {
            var parameters = new Dictionary<string, object> { { "@ID", id } };

            var dataSet = _context.ExecuteQuery("SelectNumberByID", parameters, CommandType.StoredProcedure);

            var number = ToNumber(dataSet);

            return number;
        }

        public void Create(PhoneNumber dtn)
        {
            var parameters =
                new Dictionary<string, object>
                {
                    { "@Number", dtn.Number },
                    { "@DetaineeID", dtn.DetaineeID },
                };

            _context.ExecuteNonQuery("CreateNumber", parameters, CommandType.StoredProcedure);
        }

        public void Update(PhoneNumber dtn)
        {
            var parameters =
                 new Dictionary<string, object>
                 {
                     { "@ID", dtn.NumberID },
                    { "@Number", dtn.Number },
                    { "@DetaineeID", dtn.DetaineeID },
                 };

            _context.ExecuteNonQuery("UpdateNumber", parameters, CommandType.StoredProcedure);
        }

        public void Delete(int id)
        {
            var parameters =
                new Dictionary<string, object>
                {
                    { "@ID", id },
                };

            _context.ExecuteNonQuery("DeleteNumber", parameters, CommandType.StoredProcedure);
        }

        #region Converters
        private IReadOnlyCollection<PhoneNumber> ToNumberList(DataSet dataset)
        {
            return dataset.Tables[0].AsEnumerable().Select(row=>
                new PhoneNumber
                    {
                        NumberID = row.Field<int>("NumberID"),
                        Number = row.Field<string>("Number"),
                        DetaineeID = row.Field<int>("DetaineeID")
                    }
            ).ToList();
        }

        private IReadOnlyCollection<Detainee> ToDetaineeList(DataSet dataset)
        {
           return dataset.Tables[0].AsEnumerable().Select(row=>
                new Detainee
                    {
                        DetaineeID = row.Field<int>(0),
                        LastName = row.Field<string>(1)
                    }
            ).ToList();
        }

        private PhoneNumber ToNumber(DataSet dataset)
        {
            var row = dataset.Tables[0].Rows[0];

            return new PhoneNumber
            {
                NumberID = row.Field<int>("NumberID"),
                Number = row.Field<string>("Number"),
                DetaineeID= row.Field<int>("DetaineeID")

            };
        }

        #endregion
    }
}
