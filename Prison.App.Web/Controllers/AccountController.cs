using Prison.App.Business.Services;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Web.Models;
using System.Web.Mvc;
using Prison.App.Web.Attributes;

namespace Prison.App.Web.Controllers
{
    public class AccountController : Controller
    {
        private ILogInService _logInService;

        private ILogger _log;

        public AccountController(ILogInService service,ILogger log)
        {

            ArgumentHelper.ThrowExceptionIfNull(service,"ILogInService");
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");

            _logInService = service;
            _log = log;
        }

        [OutputCache(CacheProfile = "LoginAccountCacheProfile")]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            var Model = new UserLoginViewModel();

            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginViewModel user,string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

           _logInService.LogIn(user.UserName, user.Password);

            return RedirectToLocal(ReturnUrl);
        }

        [User]
        [HttpPost]
        public ActionResult LogOut()
        {
            _logInService.LogOut();

            return RedirectToAction("Login");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}