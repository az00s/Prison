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
    public class PositionController : Controller
    {
        private ILogger _log;

        private IPositionProvider _positionProvider;

        private IPositionService _positionService;

        public PositionController(IPositionProvider positionProvider, ILogger log, IPositionService positionService)
        {
            ArgumentHelper.ThrowExceptionIfNull(positionProvider, "IPositionProvider");
            ArgumentHelper.ThrowExceptionIfNull(positionService, "IPositionService");
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");

            _positionService = positionService;
            _positionProvider = positionProvider;
            _log = log;
        }

        public ActionResult Index()
        {
            var positions = _positionProvider.GetAllPositions();
            var ViewModel = ToPositionIndexViewModel(positions);
            return View(ViewModel);
        }

        public ActionResult Details(int id)
        {
            var position = _positionProvider.GetPositionByID(id);
            var model = ToPositionViewModel(position);
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PositionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var position = ToPosition(model);

            _positionService.Create(position);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var position = _positionProvider.GetPositionByID(id);
            var ViewModel = ToPositionViewModel(position);
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult Edit(PositionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var position = ToPosition(model);
            _positionService.Update(position);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var position = _positionProvider.GetPositionByID(id);
            var model = ToPositionViewModel(position);

            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteFromDb(int id)
        {
            _positionService.Delete(id);

            return RedirectToAction("Index");
        }

        #region ViewModelHelper

        private Position ToPosition(PositionViewModel model)
        {
            return new Position
            {
                PositionID = model.PositionID,
                PositionName = model.PositionName
            };
        }

        private PositionViewModel ToPositionViewModel(Position position)
        {
            return new PositionViewModel
            {
                PositionID = position.PositionID,
                PositionName = position.PositionName
            };
        }

        private IReadOnlyCollection<PositionViewModel> ToPositionIndexViewModel(IReadOnlyCollection<Position> list)
        {
            var ResultList = new List<PositionViewModel>();
            foreach (Position item in list)
            {
                ResultList.Add(new PositionViewModel
                {
                    PositionID=item.PositionID,
                    PositionName = item.PositionName
                });
            }

            return ResultList;

        }

        #endregion
    }
}