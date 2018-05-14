using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Prison.App.Web.Helpers
{
    public static class ViewHelper
    {
        
            public static string ConvertByteArrToString(this HtmlHelper html, byte[] arr)
            {
                string base64 = Convert.ToBase64String(arr);
                string imgSrc = $"data:image;base64,{base64}";

                return imgSrc;
            }
        
    }
}
