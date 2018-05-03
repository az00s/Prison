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
    public class PlaceOfDetentionController : Controller
    {
        private static ILogger log;

        private IRepository db;

        public PlaceOfDetentionController(IRepository rep, ILogger logger)
        {

            Helper.NullChecking(rep, logger);

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