using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using System.Web.Mvc;
using Prison.App.Business.Attributes;

namespace Prison.App.Web.Controllers
{
    
    [User]
    public class HomeController : Controller
    {
        private ILogger log;

        public HomeController(ILogger logger)
        {
            ArgumentHelper.ThrowExceptionIfNull(logger, "ILogger");

            log = logger;
        }

        public ActionResult Index()
        {
            return View();
        }

        
    }
}