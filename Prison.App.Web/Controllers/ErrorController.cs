using Prison.App.Web.Models;
using System.Web.Mvc;

namespace Prison.App.Web.Controllers
{

    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult NotFound()
        {
            return View();
        }

        public ViewResult ServerError()
        {
            return View();
        }

        public ViewResult CustomError(string message)
        {

            return View("CustomError",new ErrorViewModel { Message = message });
        }

        public ViewResult Unauthorized()
        {
            return View();
        }
    }
}