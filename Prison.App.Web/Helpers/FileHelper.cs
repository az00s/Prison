using System.Web;
using System.Web.Hosting;

namespace Prison.App.Web.Helpers
{
    public static class FileHelper
    {
        private const string LOCATION_PHOTOS_DETAINEES = "/Content/Images/ProfilePhotos/";

        public static void SaveFileOnServer(HttpPostedFileBase file)
        {
            var fileName = System.IO.Path.GetFileName(file.FileName);
            var physicalPath = System.IO.Path.Combine(HostingEnvironment.MapPath("~"+LOCATION_PHOTOS_DETAINEES), fileName);
            file.SaveAs(physicalPath);
        }

        public static string GetFilePath(string fileName)
        {
            return LOCATION_PHOTOS_DETAINEES + fileName;
        }
    }
}