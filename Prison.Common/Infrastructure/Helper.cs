using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prison.Common.Infrastructure
{
    public static class Helper
    {   
        public static void NullChecking(params object[] arr)
        {
            foreach (object obj in arr)
            {
                if (obj == null) throw new ArgumentNullException();
            }
        }
    }
}
