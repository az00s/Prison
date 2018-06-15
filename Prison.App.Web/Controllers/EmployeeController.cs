using Prison.App.Web.Attributes;
using Prison.App.Business.Providers;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Web.Helpers;
using Prison.App.Web.Models;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Generic;
using System.Linq;
using Prison.App.Business.Services;
using System.Data.SqlClient;

namespace Prison.App.Web.Controllers
{
    [Editor]
    public class EmployeeController : Controller
    {
        private ILogger _log;

        private IEmployeeProvider _employeeProvider;

        private IPositionProvider _positionProvider;

        private IEmployeeService _employeeService;

        public EmployeeController(IEmployeeProvider employeeProvider, ILogger log, IPositionProvider positionProvider, IEmployeeService employeeService)
        {
            ArgumentHelper.ThrowExceptionIfNull(positionProvider, "IPositionProvider");
            ArgumentHelper.ThrowExceptionIfNull(employeeProvider, "IEmployeeProvider");
            ArgumentHelper.ThrowExceptionIfNull(employeeService, "IEmployeeService");
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");

            _positionProvider = positionProvider;
            _employeeProvider = employeeProvider;
            _employeeService = employeeService;
            _log = log;
        }

        public ActionResult Index()
        {
            var Employees = _employeeProvider.GetAllEmployees();

            var ViewModelList = ToEmployeeIndexViewModel(Employees);

            return View(ViewModelList);
        }

        public ActionResult Details(int id)
        {
            var Employee = _employeeProvider.GetEmployeeByID(id);

            var ViewModel=ToEmployeeIndexViewModel(Employee);

            return View(ViewModel);
            
        }

        [HttpGet]
        public ActionResult Create()
        {
            var positions = _positionProvider.GetAllPositions();

            var ViewModel = new EmployeeEditViewModel
            {
                Positions = positions
            };

            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult Create(EmployeeEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Positions= _positionProvider.GetAllPositions();

                return View(model);
            }

            var emp = ToEmployee(model);

            _employeeService.Create(emp);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var emp = _employeeProvider.GetEmployeeByID(id);

            var ViewModel = ToEmployeeEditViewModel(emp);

            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeEditViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                model.Positions = _positionProvider.GetAllPositions();

                return View(model);
            }

            var emp = ToEmployee(model);

            _employeeService.Update(emp);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var Employee = _employeeProvider.GetEmployeeByID(id);

            var ViewModel = ToEmployeeIndexViewModel(Employee);

            return View(ViewModel);
        }

        [HandleError(ExceptionType =typeof(SqlException),View ="EmployeeReferenceError")]
        [HttpPost]
        public ActionResult DeleteFromDb(int id)
        {
            _employeeService.Delete(id);

            return RedirectToAction("Index");
        }

        #region ViewModelHelpers

        private IEnumerable<EmployeeIndexViewModel> ToEmployeeIndexViewModel(IEnumerable<Employee> list)
        {
            var Positions = _positionProvider.GetAllPositions();

            List<EmployeeIndexViewModel> ResultList = new List<EmployeeIndexViewModel>();
            foreach (Employee item in list)
            {
                ResultList.Add(new EmployeeIndexViewModel
                {
                    EmployeeID = item.EmployeeID,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    MiddleName = item.MiddleName,
                    Position = Positions.First(p => p.PositionID == item.PositionID).PositionName
                });
            }

            return ResultList;
        }

        private EmployeeIndexViewModel ToEmployeeIndexViewModel(Employee emp)
        {
            var Positions = _positionProvider.GetAllPositions();

            EmployeeIndexViewModel Result = new EmployeeIndexViewModel
            {
                EmployeeID = emp.EmployeeID,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                MiddleName = emp.MiddleName,
                Position = Positions.First(p => p.PositionID == emp.PositionID).PositionName
            };

            return Result;
        }

        private EmployeeEditViewModel ToEmployeeEditViewModel(Employee emp)
        {
            var Positions = _positionProvider.GetAllPositions();

            EmployeeEditViewModel Result = new EmployeeEditViewModel
            {
                EmployeeID = emp.EmployeeID,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                MiddleName = emp.MiddleName,
                Positions = Positions,
                PositionID = emp.PositionID
            };

            return Result;
        }

        private Employee ToEmployee(EmployeeEditViewModel model)
        {
            Employee Result = new Employee
            {
                EmployeeID = model.EmployeeID,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                PositionID = model.PositionID
            };

            return Result;
        }


        #endregion


    }
}