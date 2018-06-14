using Prison.App.Business.Providers;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using System.Web.Mvc;
using Prison.App.Web.Attributes;
using Prison.App.Web.Models;
using System.Collections.Generic;
using Prison.App.Business.Services;
using System.Linq;

namespace Prison.App.Web.Controllers
{
    [Editor]
    public class PhoneNumberController : Controller
    {
        private ILogger _log;

        private IPhoneNumberProvider _numberProvider;

        private IPhoneNumberService _numberService;

        public PhoneNumberController(IPhoneNumberProvider numberProvider, ILogger log, IPhoneNumberService numberService)
        {
            ArgumentHelper.ThrowExceptionIfNull(numberProvider, "IPhoneNumberProvider");
            ArgumentHelper.ThrowExceptionIfNull(numberService, "IPhoneNumberService");
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");

            _numberService = numberService;
            _numberProvider = numberProvider;
            _log = log;
        }

        public ActionResult Index()
        {
            var numbers = _numberProvider.GetAllNumbers();
            var ViewModel = ToNumberIndexViewModel(numbers);
            return View(ViewModel);
        }

        public ActionResult Details(int id)
        {
            var number = _numberProvider.GetNumberByID(id);
            var model = ToNumberViewModel(number);
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new NumberViewModel
            {
                Detainees = _numberProvider.GetAllDetaineeLastNames()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(NumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var number = ToPhoneNumber(model);

            _numberService.Create(number);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var number = _numberProvider.GetNumberByID(id);
            var ViewModel = ToNumberViewModel(number);
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult Edit(NumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var number = ToPhoneNumber(model);
            _numberService.Update(number);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var number = _numberProvider.GetNumberByID(id);
            var model = ToNumberViewModel(number);
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteFromDb(int id)
        {
            _numberService.Delete(id);

            return RedirectToAction("Index");
        }

        #region ViewModelHelper

        private PhoneNumber ToPhoneNumber(NumberViewModel model)
        {
            return new PhoneNumber { NumberID = model.NumberID, Number = model.Number,DetaineeID=model.DetaineeID };
        }

        private NumberViewModel ToNumberViewModel(PhoneNumber number)
        {
            return new NumberViewModel {
                NumberID = number.NumberID,
                Number = number.Number,
                DetaineeID = number.DetaineeID,
                Detainees = _numberProvider.GetAllDetaineeLastNames(),
                DetaineeLastname = _numberProvider.GetAllDetaineeLastNames().First(l => l.DetaineeID == number.DetaineeID).LastName

            };
        }

        private IEnumerable<NumberViewModel> ToNumberIndexViewModel(IEnumerable<PhoneNumber> list)
        {
            List<NumberViewModel> ResultList = new List<NumberViewModel>();
            foreach (PhoneNumber item in list)
            {
                ResultList.Add(new NumberViewModel
                {
                    NumberID=item.NumberID,
                    Number = item.Number,
                    DetaineeID=item.DetaineeID,
                    DetaineeLastname = _numberProvider.GetAllDetaineeLastNames().First(l => l.DetaineeID == item.DetaineeID).LastName
                });
            }

            return ResultList;

        }

        #endregion
    }
}