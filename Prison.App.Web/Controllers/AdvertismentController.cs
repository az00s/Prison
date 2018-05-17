﻿using Prison.App.Business.Interfaces;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Prison.App.Web.Controllers
{
    public class AdvertismentController : Controller
    {
        private ILogger _log;

        public IAdvertismentProvider _adService;

        public AdvertismentController(ILogger log, IAdvertismentProvider adService)
        {
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");
            ArgumentHelper.ThrowExceptionIfNull(adService, "IAdvertismentProvider");

            _adService = adService;
            _log = log;
        }

        // GET: Advertisment
        public ActionResult GetAdUnit()
        {
            var listOfBlurbs = _adService.GetElementsFromRep(3);
            
            return PartialView("AddUnit",listOfBlurbs);
        }

    }
}