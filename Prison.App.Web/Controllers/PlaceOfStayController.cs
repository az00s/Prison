using Prison.App.Business.Providers;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using System.Web.Mvc;
using Prison.App.Web.Attributes;
using Prison.App.Web.Models;
using System.Collections.Generic;

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

        [User]
        public ActionResult Index()
        {
            var Places = db.GetAllRecordsFromTable();
            var ViewModel = ToPlaceOfStayIndexViewModel(Places);
            return View(ViewModel);
        }

        [Editor]
        public ActionResult Details(int id)
        {
            var place = db.GetPlaceOfStayByID(id);

            return View(place);
        }

        [Editor]
        public ActionResult Create()
        {
            return View();
        }

        [Editor]
        [HttpPost]
        public ActionResult Create(PlaceOfStayViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var place = ToPlaceOfStay(model);

            db.Create(place);

            return RedirectToAction("Index");
        }

        [Editor]
        public ActionResult Edit(int id)
        {
            var place = db.GetPlaceOfStayByID(id);
            var ViewModel = ToPlaceOfStayViewModel(place);
            return View(ViewModel);
        }

        [Editor]
        [HttpPost]
        public ActionResult Edit(PlaceOfStayViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var place = ToPlaceOfStay(model);
            db.Update(place);

            return RedirectToAction("Index");
        }

        [Editor]
        public ActionResult Delete(int id)
        {
            var place = db.GetPlaceOfStayByID(id);

            return View(place);
        }

        [Editor]
        [HttpPost]
        public ActionResult DeleteFromDb(int id)
        {
            db.Delete(id);

            return RedirectToAction("Index");
        }

        #region ViewModelHelper

        private PlaceOfStay ToPlaceOfStay(PlaceOfStayViewModel model)
        {
            return new PlaceOfStay { PlaceID = model.PlaceID, Address = model.Address };
        }

        private PlaceOfStayViewModel ToPlaceOfStayViewModel(PlaceOfStay place)
        {
            return new PlaceOfStayViewModel { PlaceID=place.PlaceID,Address = place.Address };
        }

        private IEnumerable<PlaceOfStayViewModel> ToPlaceOfStayIndexViewModel(IEnumerable<PlaceOfStay> list)
        {
            List<PlaceOfStayViewModel> ResultList = new List<PlaceOfStayViewModel>();
            foreach (PlaceOfStay item in list)
            {
                ResultList.Add(new PlaceOfStayViewModel
                {
                    PlaceID=item.PlaceID,
                    Address = item.Address
                });
            }

            return ResultList;

        }

        #endregion
    }
}