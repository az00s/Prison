using System.Web.Mvc;
using Prison.App.Web.Attributes;
using Prison.App.Business.Providers;
using Prison.App.Common.Interfaces;
using Prison.App.Common.Helpers;
using Prison.App.Common.Entities;
using Prison.App.Business.Services;
using System.Collections.Generic;
using Prison.App.Web.Models;

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

            var model = ToRoleIndexViewModel(roles);
            if (roles == null)
            {
                return RedirectToAction("Index", "Error");
            }

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = ToRole(model);
                _roleService.Create(role);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var role = _roleProvider.GetRoleByID(id);
            var model = ToRoleViewModel(role);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = ToRole(model);
                _roleService.Update(role);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var role = _roleProvider.GetRoleByID(id);
            var model = ToRoleViewModel(role);
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteFromDb(int id)
        {
                _roleService.Delete(id);

                return RedirectToAction("Index");
        }

        #region ViewModelHelpers

        private IEnumerable<RoleViewModel> ToRoleIndexViewModel(IEnumerable<Role> list)
        {
            List<RoleViewModel> ResultList = new List<RoleViewModel>();
            foreach (var item in list)
            {
                ResultList.Add(new RoleViewModel
                {
                    RoleID = item.RoleID,
                    RoleName = item.RoleName
                });
            }

            return ResultList;
        }

        private Role ToRole(RoleViewModel model)
        {
            return new Role { RoleID = model.RoleID, RoleName = model.RoleName };
        }

        private RoleViewModel ToRoleViewModel(Role role)
        {
            return new RoleViewModel { RoleID = role.RoleID, RoleName = role.RoleName };
        }

        #endregion

    }
}