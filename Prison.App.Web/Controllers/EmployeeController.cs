using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Data.Interfaces;
using System.Linq;
using System.Web.Mvc;

namespace Prison.App.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private static ILogger log;

        private IRepository db;

        public EmployeeController(IRepository rep, ILogger logger)
        {

            ArgumentHelper.ThrowExceptionIfNull(rep, "IRepository");
            ArgumentHelper.ThrowExceptionIfNull(logger, "ILogger");

            db = rep;
            log = logger;
        }
        // GET: Home
        public ActionResult Index()
        {
            var Employees = db.Employees;

            return View(Employees);
        }

        public ActionResult Details(int id)
        {
            var Employee = db.Employees.First(d => d.EmployeeID == id);

            return View(Employee);
        }
    }
}