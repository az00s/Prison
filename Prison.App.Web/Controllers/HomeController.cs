using Prison.App.Business.Providers;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Prison.App.Web.Controllers
{
    public class HomeController : Controller
    {
        private ILogger log;

        private IDataProvider db;

        public HomeController(IDataProvider rep, ILogger logger)
        {
            ArgumentHelper.ThrowExceptionIfNull(rep, "IDataProvider");
            ArgumentHelper.ThrowExceptionIfNull(logger, "ILogger");

            db = rep;
            log = logger;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetDetaineeByDate(DateTime date)
        {
            var Detainees = db.Detainees
                .GetAllRecordsFromTable()
                .Where(dt=>dt.Detentions.Any(dtn=>dtn.DetentionDate== date));

            return View("DetaineeList", Detainees);
        }
    }
}