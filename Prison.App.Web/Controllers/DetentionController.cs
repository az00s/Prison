﻿using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Data.Interfaces;
using System.Linq;
using System.Web.Mvc;

namespace Prison.App.Web.Controllers
{
    public class DetentionController : Controller
    {
        private ILogger log;

        private IRepository db;

        public DetentionController(IRepository rep, ILogger logger)
        {

            ArgumentHelper.ThrowExceptionIfNull(rep, "IRepository");
            ArgumentHelper.ThrowExceptionIfNull(logger, "ILogger");

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