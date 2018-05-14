using Prison.App.Business.Providers;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using System;
using System.Collections.Generic;
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
        // GET: Home
        public ActionResult Index()
        {
            //log.Info("Info message");
            //_rep.ErrorMethod();
            //var DetentionDates = db.Detentions.Select(d => d.DetentionDate.ToShortDateString());

            return View();
        }

        public ActionResult GetDetaineeByDate(DateTime date)
        {
            IEnumerable<Detainee> Detainees = db.Detainees.GetAllRecordsFromTable();
            var result=Detainees.Where(dt=>dt.Detentions.Any(dtn=>dtn.DetentionDate== date));

            return View("DetaineeList", result);
        }
    }
}