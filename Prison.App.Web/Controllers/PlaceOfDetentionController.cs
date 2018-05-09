using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Data.Interfaces;
using System.Web.Mvc;

namespace Prison.App.Web.Controllers
{
    public class PlaceOfDetentionController : Controller
    {
        private static ILogger log;

        private IRepository db;

        public PlaceOfDetentionController(IRepository rep, ILogger logger)
        {

            ArgumentHelper.ThrowExceptionIfNull(rep, "IRepository");
            ArgumentHelper.ThrowExceptionIfNull(logger, "ILogger");

            db = rep;
            log = logger;
        }

        public ActionResult Index()
        {
            var Places = db.PlacesOfDetention;

            return View(Places);
        }
    }
}