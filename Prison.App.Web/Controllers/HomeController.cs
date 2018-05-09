using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Data.Interfaces;
using Prison.App.Data.ServiceReference;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Web.Mvc;

namespace Prison.App.Web.Controllers
{
    public class HomeController : Controller
    {
        private ILogger log;

        private IRepository db;

        

        public HomeController(IRepository rep, ILogger logger)
        {

            ArgumentHelper.ThrowExceptionIfNull(rep, "IRepository");
            ArgumentHelper.ThrowExceptionIfNull(logger, "ILogger");

            db = rep;
            log = logger;
        }
        // GET: Home
        public ActionResult Index()
        {
            //log.Info("Info message");
            //_rep.ErrorMethod();
            var DetentionDates = db.Detentions.Select(d => d.DetentionDate.ToShortDateString());

            return View(DetentionDates);
        }

        public ActionResult GetDetaineeByDate(DateTime date)
        {
            var Detainees = db.Detainees.Where(d => d.Detentions.Any(dt => dt.DetentionDate == date));

            return View("DetaineeList",Detainees);
        }
    }
}