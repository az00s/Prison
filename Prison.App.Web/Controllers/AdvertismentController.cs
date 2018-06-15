using Prison.App.Business.Interfaces;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using System.Web.Mvc;
using System.Web;

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

        public ActionResult GetAdUnit()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //because of home page must show 3 Ad
            int blurbsCount = 3;
           
            var listOfBlurbs = _adService.GetElementsFromRep(blurbsCount);

            return PartialView("AddUnit", listOfBlurbs);
            
            
            
        }

    }
}