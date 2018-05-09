using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Data.Interfaces;
using System.Linq;
using System.Web.Mvc;

namespace Prison.App.Web.Controllers
{
    public class DetaineeController : Controller
    {
        private static ILogger log;

        private IRepository db;

        public DetaineeController(IRepository rep, ILogger logger)
        {

            ArgumentHelper.ThrowExceptionIfNull(rep, "IRepository");
            ArgumentHelper.ThrowExceptionIfNull(logger, "ILogger");

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