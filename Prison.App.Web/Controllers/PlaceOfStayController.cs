using Prison.App.Business.Providers;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using System.Web.Mvc;
using Prison.App.Web.Attributes;
using Prison.App.Web.Models;
using System.Collections.Generic;
using Prison.App.Business.Services;

namespace Prison.App.Web.Controllers
{
    [Editor]
    public class PlaceOfStayController : Controller
    {
        private ILogger _log;

        private IPlaceProvider _placeProvider;

        private IPlaceService _placeService;

        public PlaceOfStayController(IPlaceProvider placeProvider, ILogger log, IPlaceService placeService)
        {
            ArgumentHelper.ThrowExceptionIfNull(placeProvider, "IPlaceProvider");
            ArgumentHelper.ThrowExceptionIfNull(placeService, "IPlaceService");
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");

            _placeService = placeService;
            _placeProvider = placeProvider;
            _log = log;
        }

        public ActionResult Index()
        {
            var Places = _placeProvider.GetAllPlaces();
            var ViewModel = ToPlaceOfStayIndexViewModel(Places);
            return View(ViewModel);
        }

        public ActionResult Details(int id)
        {
            var place = _placeProvider.GetPlaceByID(id);

            return View(place);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PlaceOfStayViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var place = ToPlaceOfStay(model);

            _placeService.Create(place);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var place = _placeProvider.GetPlaceByID(id);
            var ViewModel = ToPlaceOfStayViewModel(place);
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult Edit(PlaceOfStayViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var place = ToPlaceOfStay(model);
            _placeService.Update(place);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var place = _placeProvider.GetPlaceByID(id);

            return View(place);
        }

        [HttpPost]
        public ActionResult DeleteFromDb(int id)
        {
            _placeService.Delete(id);

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

        private IReadOnlyCollection<PlaceOfStayViewModel> ToPlaceOfStayIndexViewModel(IReadOnlyCollection<PlaceOfStay> list)
        {
            var ResultList = new List<PlaceOfStayViewModel>();
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