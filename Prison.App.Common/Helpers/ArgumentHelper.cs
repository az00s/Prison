using System;

namespace Prison.App.Common.Helpers
{
    public static class ArgumentHelper
    {   

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
            if (id > 0 && id <=100)
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
            DateTime startDate = new DateTime(2018,5,1);
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
