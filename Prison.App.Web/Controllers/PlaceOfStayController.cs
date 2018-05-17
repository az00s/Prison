using Prison.App.Business.Providers;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using System.Linq;
using System.Web.Mvc;

namespace Prison.App.Web.Controllers
{
    public class PlaceOfStayController : Controller
    {
        private ILogger log;

        private IDataProvider db;

        public PlaceOfStayController(IDataProvider rep, ILogger logger)
        {
            ArgumentHelper.ThrowExceptionIfNull(rep, "IDataProvider");
            ArgumentHelper.ThrowExceptionIfNull(logger, "ILogger");

            db = rep;
            log = logger;
        }

        public ActionResult Index()
        {
            var Places = db.PlacesOfStay.GetAllRecordsFromTable();

            return View(Places);
        }

        public ActionResult Details(int id)
        {
            var places = db.PlacesOfStay.GetAllRecordsFromTable().First(d => d.PlaceID == id);

            return View(places);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PlaceOfStay dtn)
        {
            if (ModelState.IsValid)
            {
                db.PlacesOfStay.Create(dtn);
            }

            return View("Index", db.PlacesOfStay.GetAllRecordsFromTable());
        }

        public ActionResult Edit(int id)
        {
            var place = db.PlacesOfStay.GetAllRecordsFromTable().First(d => d.PlaceID == id);

            return View(place);
        }

        [HttpPost]
        public ActionResult Edit(PlaceOfStay dtn)
        {
            if (ModelState.IsValid)
            {
                db.PlacesOfStay.Update(dtn);
            }

            return View("Index", db.PlacesOfStay.GetAllRecordsFromTable());
        }


        public ActionResult Delete(int id)
        {
            var place = db.PlacesOfStay.GetAllRecordsFromTable().First(d => d.PlaceID == id);

            return View(place);
        }

        [HttpPost]
        public ActionResult DeleteFromDb(int id)
        {
            db.PlacesOfStay.Delete(id);

            return View("Index", db.PlacesOfStay.GetAllRecordsFromTable());
        }
    }
}