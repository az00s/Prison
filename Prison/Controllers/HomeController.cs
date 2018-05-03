using Prison.Common;
using Prison.App.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prison.Common.Interfaces;
using Prison.Common.Infrastructure;

namespace Prison.Controllers
{
    public class HomeController : Controller
    {
        private static ILogger log;

        private IRepository db;

        public HomeController(IRepository rep, ILogger logger)
        {
            
            Helper.NullChecking(rep,logger);

            db = rep;
            log = logger;
        }
        // GET: Home
        public ActionResult Index()
        {
            log.Info("Info message");

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