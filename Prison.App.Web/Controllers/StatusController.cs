﻿using Prison.App.Business.Providers;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using System.Web.Mvc;
using Prison.App.Web.Attributes;
using Prison.App.Web.Models;
using System.Collections.Generic;
using Prison.App.Business.Services;

namespace Prison.App.Web.Controllers
{
    [Editor]
    public class StatusController : Controller
    {
        private IStatusProvider _statusProvider;

        private IStatusService _statusService;

        public StatusController(IStatusProvider statusProvider, IStatusService statusService)
        {
            ArgumentHelper.ThrowExceptionIfNull(statusProvider, "IStatusProvider");
            ArgumentHelper.ThrowExceptionIfNull(statusService, "IStatusService");

            _statusService = statusService;
            _statusProvider = statusProvider;
        }

        public ActionResult Index()
        {
            var Statuses = _statusProvider.GetAllStatuses();
            var ViewModel = ToStatusIndexViewModel(Statuses);
            return View(ViewModel);
        }

        public ActionResult Details(int id)
        {
            var status = _statusProvider.GetStatusByID(id);
            var model = ToStatusViewModel(status);
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(StatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var status = ToStatus(model);

            _statusService.Create(status);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var status = _statusProvider.GetStatusByID(id);
            var ViewModel = ToStatusViewModel(status);
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult Edit(StatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var status = ToStatus(model);

            _statusService.Update(status);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var status = _statusProvider.GetStatusByID(id);
            var model = ToStatusViewModel(status);
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            _statusService.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult GetMaritalDropDown(int selectedID=0)
        {
            var list= _statusProvider.GetAllStatuses();
            var model = new MaritalDropDownViewModel {
                Statuses = list,
                SelectedID= selectedID
            };
            return View("_MaritalDropDown",model);
        }

        #region ViewModelHelper

        private MaritalStatus ToStatus(StatusViewModel model)
        {
            return new MaritalStatus
            {
                StatusID = model.StatusID,
                StatusName = model.StatusName
            };
        }

        private StatusViewModel ToStatusViewModel(MaritalStatus place)
        {
            return new StatusViewModel
            {
                StatusID =place.StatusID,
                StatusName = place.StatusName
            };
        }

        private IReadOnlyCollection<StatusViewModel> ToStatusIndexViewModel(IReadOnlyCollection<MaritalStatus> list)
        {
            var ResultList = new List<StatusViewModel>();
            foreach (MaritalStatus item in list)
            {
                ResultList.Add(new StatusViewModel
                {
                    StatusID=item.StatusID,
                    StatusName = item.StatusName
                });
            }

            return ResultList;
        }

        #endregion
    }
}