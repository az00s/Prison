using Prison.App.Data.Interfaces;
using Prison.Common;
using Prison.Common.Infrastructure;
using Prison.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prison.Controllers
{
    public class DetaineeController : Controller
    {
        private static ILogger log;

        private IRepository db;

        public DetaineeController(IRepository rep, ILogger logger)
        {

            Helper.NullChecking(rep, logger);

            db = rep;
            log = logger;
        }
        // GET: Home
        public ActionResult Index()
        {
            var Detainees = db.Detainees;

            return View(Detainees);
        }

        public ActionResult Details(int id)
        {
            var Detainee = db.Detainees.First(d=>d.DetaineeID == id);

            return View(Detainee);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Detainee dtn)
        {
            if (ModelState.IsValid)
            {
                db.Detainees.Add(dtn);
            }

            return View("Index",db.Detainees);
        }
    }
}