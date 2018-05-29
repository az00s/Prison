using Prison.App.Business.Interfaces;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using System.Web.Mvc;
using Prison.App.Business.Attributes;

namespace Prison.App.Web.Controllers
{
    [User]
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
            int blurbsCount = 3;

            if (ArgumentHelper.IsValidNumber(blurbsCount))
            {
                var listOfBlurbs = _adService.GetElementsFromRep(3);

                return PartialView("AddUnit", listOfBlurbs);
            }
            else
            {
                return RedirectToAction("Index","Error");
            }
            
            
        }

    }
}