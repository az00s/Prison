using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Data.Interfaces;
using System.Linq;
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
        // GET: Home
        //public ActionResult Index()
        //{
        //    var Detentions = db.Detentions;

        //    return View(Detentions);
        //}

        //public ActionResult Details(int id)
        //{
        //    var Detention = db.Detentions.First(d => d.DetentionID == id);

        //    return View(Detention);
        //}
    }
}