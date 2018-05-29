using System.Web.Mvc;
using Prison.App.Business.Attributes;
using Prison.App.Business.Providers;
using Prison.App.Common.Interfaces;
using Prison.App.Common.Helpers;
using Prison.App.Common.Entities;

namespace Prison.App.Web.Controllers
{
    [Admin]
    public class RoleController : Controller
    {
        private ILogger _log;

        private IRoleProvider _roles;


        public RoleController(ILogger logger, IRoleProvider roles)
        {
            ArgumentHelper.ThrowExceptionIfNull(roles, "IRoleProvider");
            ArgumentHelper.ThrowExceptionIfNull(logger, "ILogger");

            _roles = roles;
            _log = logger;
        }

        public ActionResult Index()
        {
            var roles = _roles.GetAllRecordsFromTable();


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

                _roles.Create(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                var role = _roles.GetRoleByID(id);

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
                _roles.Update(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {

                var role = _roles.GetRoleByID(id);

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
                _roles.Delete(id);

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