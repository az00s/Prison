using Prison.App.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prison.Controllers
{
    public class DetaineeController : Controller
    {
        private IRepository db;

        public DetaineeController(IRepository rep)
        {
            db = rep;
        }
        // GET: Home
        public ActionResult Index()
        {
            var Detainees = db.Detainees;

            return View(Detainees);
        }
    }
}