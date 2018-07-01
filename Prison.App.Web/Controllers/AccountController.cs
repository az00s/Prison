using Prison.App.Business.Services;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Web.Models;
using System.Web.Mvc;
using Prison.App.Web.Attributes;
using Prison.App.Common.Entities.Account;

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

        public ActionResult Login()
        {
            var Model = new LoginViewModel();

            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

           LoginResult result=_logInService.LogIn(model.UserName, model.Password);

            switch (result)
            {
                case LoginResult.Success:
                    return RedirectToAction("Index","Home");
                case LoginResult.InvalidPassword:
                    ModelState.AddModelError("", $"У {model.UserName} совсем другой пароль!");
                    return View(model);
                case LoginResult.UserNotFound:
                    ModelState.AddModelError("", $"Пользователь с имененм \"{model.UserName}\" у нас не зарегистрирован!");
                    return View(model);
                case LoginResult.Failure:
                default:
                    ModelState.AddModelError("", "Неверное имя пользователя или пароль!");
                    return View(model);
            }
        }

        [User]
        [HttpPost]
        public ActionResult LogOut()
        {
            _logInService.LogOut();

            return RedirectToAction("Login");
        }
    }
}