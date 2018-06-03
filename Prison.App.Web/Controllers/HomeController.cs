using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using System.Web.Mvc;
using Prison.App.Web.Attributes;

namespace Prison.App.Web.Controllers
{
   
    public class HomeController : Controller
    {
        private ILogger log;

        public HomeController(ILogger logger)
        {
            ArgumentHelper.ThrowExceptionIfNull(logger, "ILogger");

            log = logger;
        }

        [OutputCache(CacheProfile = "IndexHomeCacheProfile")]
        public ActionResult Index()
        {
            return View();
        }

        
    }
}