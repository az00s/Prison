using Prison.App.Business.Interfaces;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using System.Web.Mvc;

namespace Prison.App.Web.Controllers
{
    public class AdvertismentController : Controller
    {
        public IAdvertismentProvider _adService;

        public AdvertismentController(IAdvertismentProvider adService)
        {
            ArgumentHelper.ThrowExceptionIfNull(adService, "IAdvertismentProvider");

            _adService = adService;
        }

        public ActionResult GetAdUnit()
        {
            //because of home page must show 3 Ads
            int blurbsCount = 3;
           
            var listOfBlurbs = _adService.GetAds(blurbsCount);

            return PartialView("AddUnit", listOfBlurbs);
        }
    }
}