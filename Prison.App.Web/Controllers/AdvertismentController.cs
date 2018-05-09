using Prison.App.Business.Interfaces;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Data.Interfaces;
using Prison.App.Data.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
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
            ArgumentHelper.ThrowExceptionIfNull(adService, "IAdService");

            _adService = adService;
            _log = log;
        }

        // GET: Advertisment
        public ActionResult GetAdUnit()
        {
            IEnumerable<Blurb> listOfBlurbs;
            try
            {
                listOfBlurbs = _adService.GetAds();
                //throw new FaultException();
            }
            catch (FaultException ex)
            {
                _log.Error(ex.Message);

                listOfBlurbs = new List<Blurb> {
                    new Blurb { BlurbHeader = "", BlurbContent = "Мы против рекламы" }
                };
            }
            return PartialView("AddUnit",listOfBlurbs);
        }

    }
}