using Prison.App.Business.Providers;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using System.Linq;
using System.Web.Mvc;

namespace Prison.App.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private ILogger log;

        private IDataProvider db;

        public EmployeeController(IDataProvider rep, ILogger logger)
        {
            ArgumentHelper.ThrowExceptionIfNull(rep, "IDataProvider");
            ArgumentHelper.ThrowExceptionIfNull(logger, "ILogger");

            db = rep;
            log = logger;
        }
        

        public ActionResult Index()
        {
            var Employees = db.Employees.GetAllRecordsFromTable();

            return View(Employees);
        }

        public ActionResult Details(int id)
        {
            var Employee = db.Employees.GetAllRecordsFromTable().First(d => d.EmployeeID == id);

            return View(Employee);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Create(emp);
            }

            return View("Index", db.Employees.GetAllRecordsFromTable());
        }

        public ActionResult Edit(int id)
        {
            var emp = db.Employees.GetAllRecordsFromTable().First(d => d.EmployeeID == id);

            return View(emp);
        }

        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Update(emp);
            }

            return View("Index", db.Employees.GetAllRecordsFromTable());
        }


        public ActionResult Delete(int id)
        {
            var emp = db.Employees.GetAllRecordsFromTable().First(d => d.EmployeeID == id);

            return View(emp);
        }

        [HttpPost]
        public ActionResult DeleteFromDb(int id)
        {
            db.Employees.Delete(id);

            return View("Index", db.Employees.GetAllRecordsFromTable());
        }
    }
}