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

        public DetaineeController(IDetaineeProvider detaineeProvider, ILogger log, IDetaineeService detaineeService)
        {

            ArgumentHelper.ThrowExceptionIfNull(detaineeProvider, "IDetaineeProvider");
            ArgumentHelper.ThrowExceptionIfNull(detaineeService, "IDetaineeService");
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");

            _detaineeService = detaineeService;
            _detaineeProvider = detaineeProvider;
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
            var statuses = _detaineeProvider.GetAllMaritalStatuses();

            var ViewModel = new DetaineeEditViewModel
            {
                MaritalStatus = statuses,
                
            };

            return View(ViewModel);
        }

        [Editor]
        [HttpPost]
        public ActionResult Create(DetaineeEditViewModel dtn)
        {
            if (ModelState.IsValid)
            {
                var Entity = ToDetainee(dtn);
                _detaineeService.Create(Entity);
            }

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
        [HttpPost]
        public ActionResult Search(SearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("ValidationError", model);
            }

            var Detainees = _detaineeProvider.GetDetaineesByParams(model.DetentionDate, model.FirstName, model.LastName, model.Middlename, model.ResidenceAddress);
            
            var resultList = ToDetaineeIndexViewModel(Detainees);
            return View("DetaineeList", resultList);
            
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
                Detentions = dtn.Detentions,
                PhoneNumbers = dtn.PhoneNumbers
            };

            return Result;
        }

        private IEnumerable<DetaineeIndexViewModel> ToDetaineeIndexViewModel(IEnumerable<Detainee> list)
        {
            if (list==null)
            {
                return null;
            }

            List<DetaineeIndexViewModel> ResultList = new List<DetaineeIndexViewModel>();

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

        private Detainee ToDetainee(DetaineeEditViewModel dtn)
        {
            return new Detainee
            {
                DetaineeID=dtn.DetaineeID,
                FirstName=dtn.FirstName,
                LastName=dtn.LastName,
                MiddleName=dtn.MiddleName,
                BirstDate=dtn.BirstDate,
                WorkPlace=dtn.WorkPlace,
                ResidenceAddress=dtn.ResidenceAddress,
                MaritalStatusID=dtn.MaritalStatusID,
                ImagePath=dtn.ImagePath,
                PhoneNumbers=dtn.PhoneNumbers,
                AdditionalData=dtn.AdditionalData,
                Detentions=dtn.Detentions

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
                BirstDate = dtn.BirstDate.ToLocalTime(),
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


        #endregion

        

    }
}