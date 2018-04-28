using Prison.App.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Prison.Controllers
{
    public class HomeController : Controller
    {
        
        private IRepository _rep;

        public HomeController(IRepository rep)
        {
            _rep = rep;
        }
        // GET: Home
        public ActionResult Index()
        {
            //_rep.ErrorMethod();
            var DetentionDates = _rep.Detentions.Select(d => d.DetentionDate.ToShortDateString());

            return View(DetentionDates);
        }

        public ActionResult GetDetaineeByDate(DateTime date)
        {
            var Detainees = _rep.Detainees.Where(d => d.Detentions.Any(dt=>dt.DetentionDate==date));

            return View(Detainees);
        }
    }
}