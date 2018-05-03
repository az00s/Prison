using Prison.App.Data;
using Prison.App.Data.Interfaces;
using Prison.Common.Infrastructure;
using Prison.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prison.Controllers
{
    public class DetentionController : Controller
    {
        private static ILogger log;

        private IRepository db;

        public DetentionController(IRepository rep, ILogger logger)
        {

            Helper.NullChecking(rep, logger);

            db = rep;
            log = logger;
        }
        // GET: Home
        public ActionResult Index()
        {
            var Detentions = db.Detentions;

            return View(Detentions);
        }

        public ActionResult Details(int id)
        {
            var Detention = db.Detentions.First(d => d.DetentionID == id);

            return View(Detention);
        }
    }
}