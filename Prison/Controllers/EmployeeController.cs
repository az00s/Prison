﻿using Prison.App.Data.Interfaces;
using Prison.Common.Infrastructure;
using Prison.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prison.Controllers
{
    public class EmployeeController : Controller
    {
        private static ILogger log;

        private IRepository db;

        public EmployeeController(IRepository rep, ILogger logger)
        {

            Helper.NullChecking(rep, logger);

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