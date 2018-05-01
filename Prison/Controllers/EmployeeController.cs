using Prison.App.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prison.Controllers
{
    public class EmployeeController : Controller
    {
        private IRepository db;

        public EmployeeController(IRepository rep)
        {
            db = rep;
        }
        // GET: Home
        public ActionResult Index()
        {
            var Employees = db.Employees;

            return View(Employees);
        }
    }
}