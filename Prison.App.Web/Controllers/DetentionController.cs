using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using System.Web.Mvc;

namespace Prison.App.Web.Controllers
{
    public class DetentionController : Controller
    {
        private ILogger log;


        public DetentionController( ILogger logger)
        {

            ArgumentHelper.ThrowExceptionIfNull(logger, "ILogger");

            log = logger;
        }
        
    }
}