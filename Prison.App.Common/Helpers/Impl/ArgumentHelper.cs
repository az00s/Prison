using System;

namespace Prison.App.Common.Helpers
{
    public class ArgumentHelper
    {
        private const string START_DATE = "2018-05-01";

        private const int MAX_NUMBER_OF_ADS = 100;

        public static void ThrowExceptionIfNull(object obj,string name)
        {

            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
            
        }

        public static bool IsValidID(int id)
        {
            if (id > 0 && id <= int.MaxValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidNumber(int id)
        {
            if (id > 0 && id <= MAX_NUMBER_OF_ADS)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidDate(DateTime date)
        {
            DateTime startDate = DateTime.Parse(START_DATE);
            DateTime endDate = DateTime.Now;

            if (date >= startDate && date <= endDate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
