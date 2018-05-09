using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
