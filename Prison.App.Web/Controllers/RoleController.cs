using System.Web.Mvc;
using Prison.App.Web.Attributes;
using Prison.App.Business.Providers;
using Prison.App.Common.Interfaces;
using Prison.App.Common.Helpers;
using Prison.App.Common.Entities;
using Prison.App.Business.Services;

namespace Prison.App.Web.Controllers
{
    [Admin]
    public class RoleController : Controller
    {
        private ILogger _log;

        private IRoleProvider _roleProvider;

        private IRoleService _roleService;

        public RoleController(ILogger log, IRoleProvider roleProvider, IRoleService roleService)
        {
            ArgumentHelper.ThrowExceptionIfNull(roleProvider, "IRoleProvider");
            ArgumentHelper.ThrowExceptionIfNull(roleService, "IRoleService");
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");

            _roleService = roleService;
            _roleProvider = roleProvider;
            _log = log;
        }

        public ActionResult Index()
        {
            var roles = _roleProvider.GetAllRoles();


            if (roles == null)
            {
                return RedirectToAction("Index", "Error");
            }

            return View(roles);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Role model)
        {
            if (ModelState.IsValid)
            {

                _roleService.Create(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                var role = _roleProvider.GetRoleByID(id);

                return View(role);
            }
            else
            {
                _log.Warn($"RoleID {id} is not valid! Controller:{RouteData.Values["controller"]}, Action:{RouteData.Values["action"]}");

                return RedirectToAction(
                    "CustomError",
                    "Error",
                    new
                    {
                        message = $"Неверно указан идентификатор -'{id}'." +
                                    $" Пожалуйста введите целое числовое значение большее нуля."
                    });
            }
        }

        [HttpPost]
        public ActionResult Edit(Role model)
        {
            if (ModelState.IsValid)
            {
                _roleService.Update(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {

                var role = _roleProvider.GetRoleByID(id);

                return View(role);
            }
            else
            {
                _log.Warn($"RoleID {id} is not valid! Controller:{RouteData.Values["controller"]}, Action:{RouteData.Values["action"]}");

                return RedirectToAction(
                    "CustomError",
                    "Error",
                    new
                    {
                        message = $"Неверно указан идентификатор -'{id}'." +
                                    $" Пожалуйста введите целое числовое значение большее нуля."
                    });
            }
        }

        [HttpPost]
        public ActionResult DeleteFromDb(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                _roleService.Delete(id);

                return RedirectToAction("Index");
            }
            else
            {
                _log.Warn($"RoleID {id} is not valid! Controller:{RouteData.Values["controller"]}, Action:{RouteData.Values["action"]}");

                return RedirectToAction(
                    "CustomError",
                    "Error",
                    new
                    {
                        message = $"Неверно указан идентификатор -'{id}'." +
                                    $" Пожалуйста введите целое числовое значение большее нуля."
                    });
            }
        }

    }
}