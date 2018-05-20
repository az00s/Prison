using Prison.App.Business.Providers;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Prison.App.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private ILogger log;

        private IEmployeeProvider db;

        public EmployeeController(IEmployeeProvider rep, ILogger logger)
        {
            ArgumentHelper.ThrowExceptionIfNull(rep, "IEmployeeProvider");
            ArgumentHelper.ThrowExceptionIfNull(logger, "ILogger");

            db = rep;
            log = logger;
        }


        public ActionResult Index()
        {
            var Employees = db.GetAllRecordsFromTable();

            if (Employees==null)
            {
                return RedirectToAction("Index", "Error");
            }
            return View(Employees);
        }

        public ActionResult Details(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                var Employee = db.GetEmployeeByID(id);

                return View(Employee);
            }
            else
            {
                return RedirectToAction("Index", "Error");
            }
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
                db.Create(emp);
            }

            return View("Index", db.GetAllRecordsFromTable());
        }

        public ActionResult Edit(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                var emp = db.GetEmployeeByID(id);

                return View(emp);
            }
            else
            {
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            if (ModelState.IsValid)
            {
                db.Update(emp);
            }

            return View("Index", db.GetAllRecordsFromTable());
        }


        public ActionResult Delete(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                var emp = db.GetEmployeeByID(id);

                return View(emp);
            }
            else
            {
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        public ActionResult DeleteFromDb(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                db.Delete(id);

                return View("Index", db.GetAllRecordsFromTable());
            }
            else
            {
                return RedirectToAction("Index", "Error");
            }
        }
    }
}