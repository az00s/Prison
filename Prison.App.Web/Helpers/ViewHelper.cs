using Prison.App.Common.Helpers;
using System;
using System.Web.Mvc;

namespace Prison.App.Web.Helpers
{
    public static class ViewHelper
    {
        public static string ConvertByteArrToString(this HtmlHelper html, byte[] arr)
        {
            ArgumentHelper.ThrowExceptionIfNull(arr, "byte[]");

            var base64 = Convert.ToBase64String(arr);
            var imgSrc = $"data:image;base64,{base64}";

            return imgSrc;
        }
    }
}
