using Prison.App.Web.Models;
using System.Web.Mvc;
using Prison.App.Business.Attributes;


namespace Prison.App.Web.Controllers
{
    [User]
    public class ErrorController : Controller
    {
        // GET: Error
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
    }
}