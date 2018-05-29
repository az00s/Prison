using Prison.App.Business.Attributes;
using Prison.App.Business.Providers;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Web.Helpers;
using Prison.App.Web.Models;
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

        [User]
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

        [Editor]
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
                _log.Error($"UserID {id} is not valid!");

                return RedirectToAction(
                    "CustomError",
                    "Error",
                    new { message = $"Пользователь с идентификатором -'{id}' не найден. Пожалуйста введите целое числовое значение большее нуля." });
            }
        }

        [Editor]
        public ActionResult Create()
        {
            var positions = _pos.GetAllRecordsFromTable();

            if (positions == null)
            {
                return RedirectToAction("Index", "Error");
            }
            var ViewModel = new EmployeeEditViewModel
            {
                Positions = positions
            };

            return View(ViewModel);
        }

        [Editor]
        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _emp.Create(emp);
            }

            return RedirectToAction("Index");
        }

        [Editor]
        public ActionResult Edit(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                var emp = _emp.GetEmployeeByID(id);

                var ViewModel = ViewModelHelper.ToEmployeeEditViewModel(emp, _pos);

                if (ViewModel == null)
                {
                    return RedirectToAction("Index", "Error");
                }
                return View(ViewModel);
            }
            else
            {
                _log.Warn($"EmployeeID {id} is not valid! Controller:{RouteData.Values["controller"]}, Action:{RouteData.Values["action"]}");

                return RedirectToAction(
                    "CustomError",
                    "Error",
                    new
                    {
                        message = $"Неверно указан идентификатор -'{id}'." +
                                    $" Пожалуйста введите целое числовое значение большее нуля."
                    });
            }
        }

        [Editor]
        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _emp.Update(emp);
            }

            return RedirectToAction("Index");
        }

        [Editor]
        public ActionResult Delete(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                
                var Employee = _emp.GetEmployeeByID(id);

                var ViewModel = ViewModelHelper.ToEmployeeIndexViewModel(Employee, _pos);

                if (ViewModel==null)
                {
                    return RedirectToAction("Index", "Error");
                }
                return View(ViewModel);
            }
            else
            {
                _log.Warn($"EmployeeID {id} is not valid! Controller:{RouteData.Values["controller"]}, Action:{RouteData.Values["action"]}");

                return RedirectToAction(
                    "CustomError",
                    "Error",
                    new
                    {
                        message = $"Неверно указан идентификатор -'{id}'." +
                                    $" Пожалуйста введите целое числовое значение большее нуля."
                    });
            }
        }

        [Editor]
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
                _log.Warn($"EmployeeID {id} is not valid! Controller:{RouteData.Values["controller"]}, Action:{RouteData.Values["action"]}");

                return RedirectToAction(
                    "CustomError",
                    "Error",
                    new { message = $"Неверно указан идентификатор -'{id}'." +
                                    $" Пожалуйста введите целое числовое значение большее нуля." });
            }
        }

       
    }
}