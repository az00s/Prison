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

        private IPlaceOfStayProvider db;

        public PlaceOfStayController(IPlaceOfStayProvider rep, ILogger logger)
        {
            ArgumentHelper.ThrowExceptionIfNull(rep, "IPlaceOfStayProvider");
            ArgumentHelper.ThrowExceptionIfNull(logger, "ILogger");

            db = rep;
            log = logger;
        }

        public ActionResult Index()
        {
            var Places = db.GetAllRecordsFromTable();

            return View(Places);
        }

        public ActionResult Details(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                var place = db.GetPlaceOfStayByID(id);

                return View(place);
            }
            else
            {
                return RedirectToAction("Index", "Error");
            }
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
                db.Create(dtn);
            }

            return View("Index", db.GetAllRecordsFromTable());
        }

        public ActionResult Edit(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                var place = db.GetPlaceOfStayByID(id);

                return View(place);
            }
            else
            {
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        public ActionResult Edit(PlaceOfStay dtn)
        {
            if (ModelState.IsValid)
            {
                db.Update(dtn);
            }

            return View("Index", db.GetAllRecordsFromTable());
        }


        public ActionResult Delete(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                var place = db.GetPlaceOfStayByID(id);

                return View(place);
            }
            else
            {
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        public ActionResult DeleteFromDb(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                db.Delete(id);

                return View("Index", db.GetAllRecordsFromTable());
            }
            else
            {
                return RedirectToAction("Index", "Error");
            }
        }
    }
}