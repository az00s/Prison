using Prison.App.Business.Providers;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using System.Linq;
using System.Web.Mvc;

namespace Prison.App.Web.Controllers
{
    public class DetaineeController : Controller
    {
        private static ILogger log;

        private IDataProvider db;

        public DetaineeController(IDataProvider rep, ILogger logger)
        {

            ArgumentHelper.ThrowExceptionIfNull(rep, "IDataProvider");
            ArgumentHelper.ThrowExceptionIfNull(logger, "ILogger");

            db = rep;
            log = logger;
        }
        // GET: Home
        public ActionResult Index()
        {
            var Detainees = db.Detainees.GetAllRecordsFromTable();

            return View(Detainees);
        }

        public ActionResult Details(int id)
        {
            var Detainee = db.Detainees.GetAllRecordsFromTable().First(d=>d.DetaineeID == id);

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
                db.Detainees.Create(dtn);
            }

            return View("Index", db.Detainees.GetAllRecordsFromTable());
        }

        public ActionResult Edit(int id)
        {

            var Detainee = db.Detainees.GetAllRecordsFromTable().First(d => d.DetaineeID == id);

            return View(Detainee);
        }

        [HttpPost]
        public ActionResult Edit(Detainee dtn)
        {
            if (ModelState.IsValid)
            {
                db.Detainees.Update(dtn);
            }

            return View("Index", db.Detainees.GetAllRecordsFromTable());
        }

       
        public ActionResult Delete(int id)
        {

            var Detainee = db.Detainees.GetAllRecordsFromTable().First(d => d.DetaineeID == id);

            return View(Detainee);
        }

        [HttpPost]
        public ActionResult DeleteFromDb(int id)
        {
           
                db.Detainees.Delete(id);
            

            return View("Index", db.Detainees.GetAllRecordsFromTable());
        }
    }
}