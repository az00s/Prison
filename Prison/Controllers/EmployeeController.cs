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
        private IRepository _rep;

        public EmployeeController(IRepository rep)
        {
            _rep = rep;
        }
        // GET: Home
        public ActionResult Index()
        {
            var Employees = _rep.Employees;

            return View(Employees);
        }
    }
}