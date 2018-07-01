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

        private IDetentionProvider _detentionProvider;

        private IDetentionService _detentionService;

        private IPlaceProvider _placeProvider;

        private IEmployeeProvider _employeeProvider;

        public DetaineeController(IDetaineeProvider detaineeProvider, ILogger log, IDetaineeService detaineeService, IPlaceProvider placeProvider, IEmployeeProvider employeeProvider, IDetentionProvider detentionProvider,IDetentionService detentionService)
        {
            ArgumentHelper.ThrowExceptionIfNull(detaineeProvider, "IDetaineeProvider");
            ArgumentHelper.ThrowExceptionIfNull(detaineeService, "IDetaineeService");
            ArgumentHelper.ThrowExceptionIfNull(detentionProvider, "IDetentionProvider");
            ArgumentHelper.ThrowExceptionIfNull(detentionService, "IDetentionService");
            ArgumentHelper.ThrowExceptionIfNull(placeProvider, "IPlaceProvider");
            ArgumentHelper.ThrowExceptionIfNull(employeeProvider, "IEmployeeProvider");
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");

            _detaineeService = detaineeService;
            _detaineeProvider = detaineeProvider;
            _detentionProvider = detentionProvider;
            _detentionService = detentionService;
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
        public ActionResult DeleteConfirmed(int id)
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
        public ActionResult CreateDetention(int id=0)
        {
            var model = new DetentionCreateViewModel {
                DetaineeID=id,
                Places = _placeProvider.GetAllPlaces(),
                Employees=_employeeProvider.GetAllEmployees()
            };

            if (id > 0)
            {
                return View("CreateDetentionModal", model);
            }

            return View("CreateDetention",model);
        }

        [Editor]
        [HttpPost]
        public ActionResult CreateDetention(DetentionCreateViewModel model)
        {
            var detention = ToDetention(model);

            _detentionService.Create(detention);

            return RedirectToAction("Details",new { id=model.DetaineeID});
        }


        [Editor]
        [HttpGet]
        public ActionResult GetDetentions()
        {
            var list = _detentionProvider.GetDetentionsForLast3Days();
            var model = ToDetentionDropDownViewModel(list);
            return View("_DetentionsField", model);
        }

        [Editor]
        public ActionResult ReleaseDetainee(int id)
        {
            var model = new ReleaseDetaineeViewModel
            {
                DetaineeID = id,
                DetentionID = _detentionProvider.GetLast(id).DetentionID,
                Employees = _employeeProvider.GetAllEmployees()
            };
            return View("ReleaseDetainee", model);
        }

        [Editor]
        [HttpPost]
        public ActionResult ReleaseDetainee(ReleaseDetaineeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var release = ToRelease(model);

                _detaineeService.ReleaseDetainee(release);
            }

            return RedirectToAction("Details",new { id=model.DetaineeID});
        }

        [User]
        public ActionResult DetentionDetails(int detentionID,int detaineeID)
        {
            var detention = _detentionProvider.GetByID(detentionID);
            var release = _detaineeProvider.GetRelease(detaineeID, detentionID);
            var model = ToDetentionDetailsViewModel(detention, release);
            return View(model);
        }

        #region ModelViewHelpers
        private DetaineeDetailsViewModel ToDetaineeDetailsViewModel(Detainee dtn)
        {
            var statuses = _detaineeProvider.GetAllMaritalStatuses();
            var lastRelease = _detaineeProvider.GetLastRelease(dtn.DetaineeID);
            var lastDetention = _detentionProvider.GetLast(dtn.DetaineeID);

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
                PhoneNumbers = dtn.PhoneNumbers,
                IsReleased= lastRelease == null?false: (lastRelease.ReleasеDate.CompareTo(lastDetention.DetentionDate) < 0 ? false : true)
            };

            return Result;
        }

        private IReadOnlyCollection<DetaineeIndexViewModel> ToDetaineeIndexViewModel(IEnumerable<Detainee> list)
        {
            if (list == null)
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

        private Release ToRelease(ReleaseDetaineeViewModel model)
        {
            return new Release
            {
                DetaineeID=model.DetaineeID,
                DetentionID = model.DetentionID,
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

        private IReadOnlyCollection<DetentionListViewModel> ToDetentionListViewModel(IEnumerable<Detention> list)
        {
            var ResultList = new List<DetentionListViewModel>();

            foreach (var item in list)
            {
                ResultList.Add(new DetentionListViewModel
                {
                    DetentionID = item.DetentionID,
                    DetentionDate = item.DetentionDate,
                    Employee = _employeeProvider.GetEmployeeByID(item.DetainedByWhomID).LastName,
                    DeliveryDate=item.DeliveryDate,
                    DeliveredByWhomID=item.DeliveredByWhomID,
                    PlaceID=item.PlaceID,
                });
            }

            return ResultList;
        }

        private Detention ToDetention(DetentionCreateViewModel model)
        {

            return new Detention
            {
                DetentionID = model.DetaineeID,
                DetentionDate = model.DetentionDate,
                DetainedByWhomID=model.DetainedByWhomID,
                DeliveryDate = model.DeliveryDate,
                DeliveredByWhomID = model.DeliveredByWhomID,
                PlaceID = model.PlaceID
            };
        }

        private DetentionDetailsViewModel ToDetentionDetailsViewModel(Detention detention,Release release)
        {
               return new DetentionDetailsViewModel
               {
                   DetentionID = detention.DetentionID,
                   DetentionDate = detention.DetentionDate,
                   DeliveryDate = detention.DeliveryDate,
                   DeliveredByWhom = _employeeProvider.GetEmployeeByID(detention.DeliveredByWhomID).LastName,
                   DetainedByWhom = _employeeProvider.GetEmployeeByID(detention.DetainedByWhomID).LastName,
                   Place = _placeProvider.GetPlaceByID(detention.PlaceID).Address,
                   ReleasеDate= release == null ? "-" : release.ReleasеDate.ToString(),
                   ReleasedByWhom= release == null ? "-" : _employeeProvider.GetEmployeeByID(release.ReleasedByWhomID).LastName,
                   AmountForStaying=release==null?0:release.AmountForStaying,
                   PaidAmount = release == null ? 0 : release.PaidAmount
                };
        }

        #endregion
    }
}