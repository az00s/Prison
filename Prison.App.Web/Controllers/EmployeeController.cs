using Prison.App.Business.Providers;
using Prison.App.Business.Providers.Impl;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Web.Helpers;
using Prison.App.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Prison.App.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private ILogger _log;

        private IEmployeeProvider _emp;

        private IPositionProvider _pos;

        public EmployeeController(IEmployeeProvider rep, ILogger logger, IPositionProvider pos)
        {
            ArgumentHelper.ThrowExceptionIfNull(pos, "IPositionProvider");
            ArgumentHelper.ThrowExceptionIfNull(rep, "IEmployeeProvider");
            ArgumentHelper.ThrowExceptionIfNull(logger, "ILogger");

            _pos = pos;
            _emp = rep;
            _log = logger;
        }


        public ActionResult Index()
        {
            var Employees = _emp.GetAllRecordsFromTable();

            var ViewModelList = ViewModelHelper.ToEmployeeIndexViewModel(Employees,_pos);

            if (ViewModelList == null)
            {
                return RedirectToAction("Index", "Error");
            }
            return View(ViewModelList);
        }

        public ActionResult Details(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                var Employee = _emp.GetEmployeeByID(id);

                var ViewModel=ViewModelHelper.ToEmployeeIndexViewModel(Employee, _pos);

                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Index", "Error");
            }
        }

        public ActionResult Create()
        {
            var positions = _pos.GetAllRecordsFromTable();

            var ViewModel = new EmployeeEditViewModel
            {
                Positions = positions
            };

            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _emp.Create(emp);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                var emp = _emp.GetEmployeeByID(id);

                var ViewModel = ViewModelHelper.ToEmployeeEditViewModel(emp, _pos);

                return View(ViewModel);
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
                _emp.Update(emp);
            }

            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                var Employee = _emp.GetEmployeeByID(id);

                var ViewModel = ViewModelHelper.ToEmployeeIndexViewModel(Employee, _pos);

                return View(ViewModel);
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
                _emp.Delete(id);

                return RedirectToAction("Index");
            }
            else
            {
                _log.Error("EmployeeID is not valid!");

                return RedirectToAction(
                    "CustomError",
                    "Error", 
                    new { message = "Указан неверный идентификатор пользователя. Пожалуйста введите целое числовое значение большее нуля." });
            }
        }

       
    }
}