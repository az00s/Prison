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

            var ViewModelList = ToEmployeeIndexViewModel(Employees);

            return View(ViewModelList);
        }

        [Editor]
        public ActionResult Details(int id)
        {
            var Employee = _emp.GetEmployeeByID(id);

            var ViewModel=ToEmployeeIndexViewModel(Employee);

            return View(ViewModel);
            
        }

        [Editor]
        [HttpGet]
        public ActionResult Create()
        {
            var positions = _pos.GetAllRecordsFromTable();

            var ViewModel = new EmployeeEditViewModel
            {
                Positions = positions
            };

            return View(ViewModel);
        }

        
        public ActionResult Create(EmployeeEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Positions= _pos.GetAllRecordsFromTable();

                return View(model);
            }

            var emp = ToEmployee(model);

            _emp.Create(emp);

            return RedirectToAction("Index");
        }

        [Editor]
        public ActionResult Edit(int id)
        {
            var emp = _emp.GetEmployeeByID(id);

            var ViewModel = ToEmployeeEditViewModel(emp);

            return View(ViewModel);
        }

        [Editor]
        [HttpPost]
        public ActionResult Edit(EmployeeEditViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                model.Positions = _pos.GetAllRecordsFromTable();

                return View(model);
            }

            var emp = ToEmployee(model);

            _emp.Update(emp);

            return RedirectToAction("Index");
        }

        [Editor]
        public ActionResult Delete(int id)
        {
            var Employee = _emp.GetEmployeeByID(id);

            var ViewModel = ToEmployeeIndexViewModel(Employee);

            return View(ViewModel);
        }

        [Editor]
        [HttpPost]
        public ActionResult DeleteFromDb(int id)
        {
            _emp.Delete(id);

            return RedirectToAction("Index");
        }

        #region ViewModelHelpers

        private IEnumerable<EmployeeIndexViewModel> ToEmployeeIndexViewModel(IEnumerable<Employee> list)
        {
            var Positions = _pos.GetAllRecordsFromTable();

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
            var Positions =_pos.GetAllRecordsFromTable();

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
            var Positions = _pos.GetAllRecordsFromTable();

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