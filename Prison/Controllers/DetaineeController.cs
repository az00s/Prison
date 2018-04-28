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
        private IRepository _rep;

        public DetaineeController(IRepository rep)
        {
            _rep = rep;
        }
        // GET: Home
        public ActionResult Index()
        {
            var Detainees = _rep.Detainees;

            return View(Detainees);
        }
    }
}