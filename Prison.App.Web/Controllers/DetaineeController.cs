using Prison.App.Business.Providers;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Web.Helpers;
using Prison.App.Web.Models;
using System;
using System.Web;
using System.Web.Mvc;
using Prison.App.Web.Attributes;
using System.Linq;
using System.Collections.Generic;
using Prison.App.Business.Services;

namespace Prison.App.Web.Controllers
{

    public class DetaineeController : Controller
    {
        private  ILogger _log;

        private IDetaineeProvider _detaineeProvider;

        private IDetaineeService _detaineeService;

        private IPlaceProvider _placeProvider;

        private IEmployeeProvider _employeeProvider;


        public DetaineeController(IDetaineeProvider detaineeProvider, ILogger log, IDetaineeService detaineeService, IPlaceProvider placeProvider, IEmployeeProvider employeeProvider)
        {

            ArgumentHelper.ThrowExceptionIfNull(detaineeProvider, "IDetaineeProvider");
            ArgumentHelper.ThrowExceptionIfNull(detaineeService, "IDetaineeService");
            ArgumentHelper.ThrowExceptionIfNull(placeProvider, "IPlaceProvider");
            ArgumentHelper.ThrowExceptionIfNull(employeeProvider, "IEmployeeProvider");
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");

            _detaineeService = detaineeService;
            _detaineeProvider = detaineeProvider;
            _placeProvider = placeProvider;
            _employeeProvider = employeeProvider;
            _log = log;
        }

        
        [User]
        public ActionResult Index()
        {
            var Detainees = _detaineeProvider.GetAllDetainees();

            var ViewModel = ToDetaineeIndexViewModel(Detainees);

            return View(ViewModel);
        }
        
        [User]
        public ActionResult Details(int id)
        {
            var Detainee = _detaineeProvider.GetDetaineeByID(id);

            var ViewModel =ToDetaineeDetailsViewModel(Detainee);

            return View(ViewModel);
            
        }

        [Editor]
        public ActionResult Create()
        {

            return View();
        }

        [Editor]
        [HttpPost]
        public ActionResult Create(DetaineeEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var detainee = ToDetainee(model);

                _detaineeService.Create(detainee);
            

            return RedirectToAction("Index");
        }

        [Editor]
        public ActionResult Edit(int id)
        {
            var Detainee = _detaineeProvider.GetDetaineeByID(id);

            var ViewModel = ToDetaineeEditViewModel(Detainee);

            return View(ViewModel);
        }

        [Editor]
        [HttpPost]
        public ActionResult Edit(DetaineeEditViewModel dtn, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    FileHelper.SaveFileOnServer(file);
                    
                    dtn.ImagePath = FileHelper.GetFilePath(file.FileName);

                }

                var Entity = ToDetainee(dtn);

                _detaineeService.Update(Entity);
            }

            return RedirectToAction("Index");
        }

        [Editor]
        public ActionResult Delete(int id)
        {
            var Detainee = _detaineeProvider.GetDetaineeByID(id);
            var ViewModel = ToDetaineeEditViewModel(Detainee);
            return View(ViewModel);
        }

        [Editor]
        [HttpPost]
        public ActionResult DeleteFromDb(int id)
        {
            _detaineeService.Delete(id);

            return RedirectToAction("Index");
        }
        
        [User]
        public ActionResult GetDetaineeByDate(DateTime date)
        {
            var Detainees = _detaineeProvider.GetDetaineesByDate(date);
            var ViewModel = ToDetaineeIndexViewModel(Detainees);
            return View("DetaineeList", ViewModel);
        }

        [User]
        public ActionResult Search()
        {
            return View();
        }

        [User]
        public ActionResult FindDetainees(SearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("ValidationError", model);
            }

            var Detainees = _detaineeProvider.Find(model.DetentionDate, model.FirstName, model.LastName, model.Middlename, model.ResidenceAddress);
            
            var resultList = ToDetaineeIndexViewModel(Detainees);
            return View("DetaineeList", resultList);
            
        }

        [Editor]
        public ActionResult CreateDetention()
        {
            var model = new DetentionCreateViewModel {
                Places = _placeProvider.GetAllPlaces(),
                Employees=_employeeProvider.GetAllEmployees()
            };
            return View("CreateDetention",model);
        }

        [Editor]
        public ActionResult GetDetentions()
        {
            var list = _detaineeProvider.GetAllDetentions();
            var model = ToDetentionDropDownViewModel(list);
            return View("_DetentionsField", model);
        }

        [Editor]
        public ActionResult ReleaseDetainee(int id)
        {
            var model = new DetentionReleaseDetaineeViewModel
            {
                DetentionID = _detaineeProvider.GetLastDetention(id).DetentionID,
                Employees = _employeeProvider.GetAllEmployees()
            };
            return View("ReleaseDetainee", model);
        }

        [Editor]
        [HttpPost]
        public ActionResult ReleaseDetainee(DetentionReleaseDetaineeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var detention = ToDetention(model);

                _detaineeService.ReleaseDetainee(detention);
            }

            return RedirectToAction("Index");
        }

        [User]
        public ActionResult DetentionDetails(int id)
        {
            var detention = _detaineeProvider.GetDetentionByID(id);
            var model = ToDetentionDetailsViewModel(detention);
            return View(model);
        }

        #region ModelViewHelpers
        private DetaineeDetailsViewModel ToDetaineeDetailsViewModel(Detainee dtn)
        {
            var statuses = _detaineeProvider.GetAllMaritalStatuses();

            DetaineeDetailsViewModel Result = new DetaineeDetailsViewModel
            {
                DetaineeID = dtn.DetaineeID,
                FirstName = dtn.FirstName,
                LastName = dtn.LastName,
                MiddleName = dtn.MiddleName,
                BirstDate = dtn.BirstDate.ToLongDateString(),
                MaritalStatus = statuses.First(s => s.StatusID == dtn.MaritalStatusID).StatusName,
                ImagePath = dtn.ImagePath,
                WorkPlace = dtn.WorkPlace,
                ResidenceAddress = dtn.ResidenceAddress,
                AdditionalData = dtn.AdditionalData,
                Detentions = ToDetentionListViewModel(dtn.Detentions),
                PhoneNumbers = dtn.PhoneNumbers
            };

            return Result;
        }

        private IReadOnlyCollection<DetaineeIndexViewModel> ToDetaineeIndexViewModel(IEnumerable<Detainee> list)
        {
            if (list==null)
            {
                return null;
            }

            var ResultList = new List<DetaineeIndexViewModel>();

            foreach (Detainee item in list)
            {
                ResultList.Add(new DetaineeIndexViewModel
                {
                    DetaineeID = item.DetaineeID,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    MiddleName = item.MiddleName,
                    BirstDate = item.BirstDate.ToShortDateString(),
                    ImagePath = item.ImagePath
                });
            }



            return ResultList;
        }

        private IReadOnlyCollection<DetentionDropDownViewModel> ToDetentionDropDownViewModel(IEnumerable<Detention> list)
        {
            if (list == null)
            {
                return null;
            }

            var ResultList = new List<DetentionDropDownViewModel>();
            var employees = _employeeProvider.GetAllEmployees();
            foreach (Detention item in list)
            {
                ResultList.Add(new DetentionDropDownViewModel
                {
                    DetentionID = item.DetentionID,
                    DetentionHeader=$"№{item.DetentionID} от {item.DetentionDate.ToShortDateString()}, Задерживал: {employees.First(e=>e.EmployeeID==item.DetainedByWhomID).LastName}"
                });
            }

            return ResultList;
        }

        private Detainee ToDetainee(DetaineeEditViewModel dtn)
        {
            return new Detainee
            {
                DetaineeID=dtn.DetaineeID,
                FirstName=dtn.FirstName,
                LastName=dtn.LastName,
                MiddleName=dtn.MiddleName,
                BirstDate=DateTime.Parse(dtn.BirstDate),
                WorkPlace=dtn.WorkPlace,
                ResidenceAddress=dtn.ResidenceAddress,
                MaritalStatusID=dtn.MaritalStatusID,
                ImagePath=dtn.ImagePath,
                PhoneNumbers=dtn.PhoneNumbers,
                AdditionalData=dtn.AdditionalData,
                Detentions=dtn.Detentions

            };
        }

        private Detention ToDetention(DetentionReleaseDetaineeViewModel model)
        {
            return new Detention
            {
                DetentionID=model.DetentionID,
                ReleasedByWhomID = model.ReleasedByWhomID,
                ReleasеDate = model.ReleasеDate,
                AmountForStaying = model.AmountForStaying,
                PaidAmount = model.PaidAmount,
            };
        }

        private DetaineeEditViewModel ToDetaineeEditViewModel(Detainee dtn)
        {
            var statuses = _detaineeProvider.GetAllMaritalStatuses();

            DetaineeEditViewModel Result = new DetaineeEditViewModel
            {
                DetaineeID = dtn.DetaineeID,
                FirstName = dtn.FirstName,
                LastName = dtn.LastName,
                MiddleName = dtn.MiddleName,
                BirstDate = dtn.BirstDate.ToShortDateString(),
                MaritalStatus = statuses,
                MaritalStatusID = dtn.MaritalStatusID,
                ImagePath = dtn.ImagePath,
                WorkPlace = dtn.WorkPlace,
                ResidenceAddress = dtn.ResidenceAddress,
                AdditionalData = dtn.AdditionalData,
                Detentions = dtn.Detentions,
                PhoneNumbers = dtn.PhoneNumbers
            };

            return Result;
        }

        private IReadOnlyCollection<DetentionListViewModel> ToDetentionListViewModel(IReadOnlyCollection<Detention> list)
        {
            if (list == null)
            {
                return null;
            }

            var ResultList = new List<DetentionListViewModel>();

            foreach (var item in list)
            {
                ResultList.Add(new DetentionListViewModel
                {
                    DetentionID = item.DetentionID,
                    DetentionDate = item.DetentionDate,
                    Employee = _employeeProvider.GetEmployeeByID(item.DetainedByWhomID).LastName,
                    ReleasеDate=item.ReleasеDate,
                    ReleasedByWhomID=item.ReleasedByWhomID,
                    DeliveryDate=item.DeliveryDate,
                    DeliveredByWhomID=item.DeliveredByWhomID,
                    PlaceID=item.PlaceID,
                    AmountForStaying=item.AmountForStaying,
                    PaidAmount=item.PaidAmount
                });
            }



            return ResultList;
        }

        private DetentionDetailsViewModel ToDetentionDetailsViewModel(Detention detention)
        {
            if (detention == null)
            {
                return null;
            }
               return new DetentionDetailsViewModel
               {
                   DetentionID = detention.DetentionID,
                   DetentionDate = detention.DetentionDate,
                   DeliveryDate = detention.DeliveryDate,
                   DeliveredByWhom = _employeeProvider.GetEmployeeByID(detention.DeliveredByWhomID).LastName,
                   DetainedByWhom = _employeeProvider.GetEmployeeByID(detention.DetainedByWhomID).LastName,
                   ReleasеDate = detention.ReleasеDate==DateTime.MinValue?"-": detention.ReleasеDate.ToShortDateString(),
                   ReleasedByWhom = detention.ReleasedByWhomID==0?"-":_employeeProvider.GetEmployeeByID(detention.ReleasedByWhomID).LastName,
                   Place = _placeProvider.GetPlaceByID(detention.PlaceID).Address,
                   PaidAmount=detention.PaidAmount,
                   AmountForStaying=detention.AmountForStaying
                };
        }

        #endregion



    }
}