using Prison.App.Business.Providers;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult Index()
        {
            var Detainees = db.GetAllRecordsFromTable();

            return View(Detainees);
        }

        public ActionResult Details(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                var Detainee = db.GetDetaineeByID(id);

                return View(Detainee);
            }
            else
            {
                return RedirectToAction("","");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Detainee dtn)
        {
            if (ModelState.IsValid)
            {
                db.Create(dtn);
            }

            return View("Index", db.GetAllRecordsFromTable());
        }

        public ActionResult Edit(int id)
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

            return View("Index", db.GetAllRecordsFromTable());
        }


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

        [HttpPost]
        public ActionResult DeleteFromDb(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                db.Delete(id);

                return View("Index", db.GetAllRecordsFromTable());
            }
            else
            {
                return RedirectToAction("Index", "Error");
            }
        }

        public ActionResult GetDetaineeByDate(DateTime date)
        {
            if (ArgumentHelper.IsValidDate(date))
            {
                var Detainees = db.GetDetaineesByDate(date);

                return View("DetaineeList", Detainees);
            }
            else
            {
                return RedirectToAction("Index","Error");
            }
        }

    }
}