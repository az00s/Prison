using Prison.App.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prison.Controllers
{
    public class DetentionController : Controller
    {
        private IRepository db;

        public DetentionController(IRepository rep)
        {
            db = rep;
        }
        // GET: Home
        public ActionResult Index()
        {
            var Detentions = db.Detentions;

            return View(Detentions);
        }
    }
}