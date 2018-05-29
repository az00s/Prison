using Prison.App.Business.Providers;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Web.Helpers;
using Prison.App.Web.Models;
using System;
using System.Web;
using System.Web.Mvc;
using Prison.App.Business.Attributes;

namespace Prison.App.Web.Controllers
{
    
    public class DetaineeController : Controller
    {
        private  ILogger log;

        private IDetaineeProvider db;

        public DetaineeController(IDetaineeProvider rep, ILogger logger)
        {

            ArgumentHelper.ThrowExceptionIfNull(rep, "IDetaineeProvider");
            ArgumentHelper.ThrowExceptionIfNull(logger, "ILogger");

            db = rep;
            log = logger;
        }

        
        [User]
        public ActionResult Index()
        {
            var Detainees = db.GetAllRecordsFromTable();

            var resultList=ViewModelHelper.ToDetaineeIndexViewModel(Detainees);

            return View(resultList);
        }
        
        [User]
        public ActionResult Details(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                var Detainee = db.GetDetaineeByID(id);

                var ViewModel=ViewModelHelper.ToDetaineeDetailsViewModel(Detainee,db);
                 
                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Index","Error");
            }
        }

        [Editor]
        public ActionResult Create()
        {
            var statuses = db.GetAllMaritalStatusesFromTable();

            var ViewModel = new DetaineeEditViewModel
            {
                MaritalStatus = statuses,
                
            };

            return View(ViewModel);
        }

        [Editor]
        [HttpPost]
        public ActionResult Create(Detainee dtn)
        {
            if (ModelState.IsValid)
            {
                db.Create(dtn);
            }

            return RedirectToAction("Index");
        }

        [Editor]
        public ActionResult Edit(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                var Detainee = db.GetDetaineeByID(id);

                var ViewModel = ViewModelHelper.ToDetaineeEditViewModel(Detainee,db);

                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Index", "Error");
            }
        }

        [Editor]
        [HttpPost]
        public ActionResult Edit(Detainee dtn, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Content/Images/ProfilePhotos"), pic);
                    file.SaveAs(path);
                    string FilePath = "/Content/Images/ProfilePhotos/" + pic;
                    dtn.ImagePath = FilePath;
                }
                db.Update(dtn);
            }

            return RedirectToAction("Index");
        }

        [Editor]
        public ActionResult Delete(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                var Detainee = db.GetDetaineeByID(id);

                return View(Detainee);
            }
            else
            {
                return RedirectToAction("Index", "Error");
            }
        }

        [Editor]
        [HttpPost]
        public ActionResult DeleteFromDb(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                db.Delete(id);

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Error");
            }
        }
        
        [User]
        public ActionResult GetDetaineeByDate(DateTime date)
        {
            if (ArgumentHelper.IsValidDate(date))
            {
                var Detainees = db.GetDetaineesByDate(date);
                var resultList = ViewModelHelper.ToDetaineeIndexViewModel(Detainees);
                return View("DetaineeList", resultList);
            }
            else
            {
                return RedirectToAction("Index","Error");
            }
        }

    }
}