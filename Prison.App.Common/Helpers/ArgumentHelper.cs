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
    }
}
