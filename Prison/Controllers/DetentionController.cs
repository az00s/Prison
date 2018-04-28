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
        private IRepository _rep;

        public DetentionController(IRepository rep)
        {
            _rep = rep;
        }
        // GET: Home
        public ActionResult Index()
        {
            var Detentions = _rep.Detentions;

            return View(Detentions);
        }
    }
}