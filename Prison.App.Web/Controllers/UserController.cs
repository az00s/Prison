using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Prison.App.Web.Attributes;
using Prison.App.Business.Providers;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Web.Models.User;
using Prison.App.Business.Services;

namespace Prison.App.Web.Controllers
{
    [Admin]
    public class UserController : Controller
    {
        private ILogger _log;

        private IUserProvider _userProvider;

        private IUserService _userService;

        private IRoleProvider _roleProvider;

        public UserController(IUserProvider userProvider, ILogger log, IRoleProvider roleProvider, IUserService userService)
        {
            ArgumentHelper.ThrowExceptionIfNull(roleProvider, "IRoleProvider");
            ArgumentHelper.ThrowExceptionIfNull(userProvider, "IUserProvider");
            ArgumentHelper.ThrowExceptionIfNull(userService, "IUserService");
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");

            _roleProvider = roleProvider;
            _userProvider = userProvider;
            _userService = userService;
            _log = log;
        }

        public ActionResult Index()
        {
            var users = _userProvider.GetAllUsers();

            var ViewModelList = ToUserIndexViewModel(users);

            if (ViewModelList == null)
            {
                return RedirectToAction("Index", "Error");
            }

            return View(ViewModelList);
        }

        public ActionResult Create()
        {
            var Roles = _roleProvider.GetAllRoles();
            var EmployeeNames = _userProvider.GetUnoccupiedEmployeeNames();

            if (EmployeeNames == null)
            {
                return RedirectToAction(
                        "CustomError",
                        "Error",
                        new
                        {
                            message ="Все сотрудники уже имеют учетные записи.Чтобы добавить новую учетную запись нужно создать нового сотрудника"
                        });

            }

            if (Roles == null)
            {
                return RedirectToAction("Index", "Error");
            }
            var ViewModel = new UserCreateViewModel
            {
                UserName=EmployeeNames,
                Roles = Roles.ToArray()
            };

            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult Create(UserCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = ToUser(model);

            _userService.Create(user);
            
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                var User = _userProvider.GetUserByID(id);

                var ViewModel = ToUserEditViewModel(User);

                return View(ViewModel);
            }
            else
            {
                _log.Error($"UserID {id} is not valid!");

                return RedirectToAction(
                    "CustomError",
                    "Error",
                    new { message = $"Пользователь с идентификатором -'{id}' не найден. Пожалуйста введите целое числовое значение большее нуля." });
            }
        }

        public ActionResult Edit(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                var User = _userProvider.GetUserByID(id);

                var ViewModel = ToUserEditViewModel(User);

                if (ViewModel == null)
                {
                    return RedirectToAction("Index", "Error");
                }
                return View(ViewModel);
            }
            else
            {
                _log.Warn($"UserID {id} is not valid! Controller:{RouteData.Values["controller"]}, Action:{RouteData.Values["action"]}");

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
        public ActionResult Edit(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = ToUser(model);
            _userService.Update(user);
           
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {

                var User = _userProvider.GetUserByID(id);

                var ViewModel = ToUserEditViewModel(User);

                if (ViewModel == null)
                {
                    return RedirectToAction("Index", "Error");
                }
                return View(ViewModel);
            }
            else
            {
                _log.Warn($"UserID {id} is not valid! Controller:{RouteData.Values["controller"]}, Action:{RouteData.Values["action"]}");

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
                _userService.Delete(id);

                return RedirectToAction("Index");
            }
            else
            {
                _log.Warn($"UserID {id} is not valid! Controller:{RouteData.Values["controller"]}, Action:{RouteData.Values["action"]}");

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

        #region ModelViewHelpers

        private IEnumerable<UserIndexViewModel> ToUserIndexViewModel(IEnumerable<User> list)
        {
            if (list == null)
            {
                return null;
            }

            List<UserIndexViewModel> ResultList = new List<UserIndexViewModel>();

            foreach (var item in list)
            {
                ResultList.Add(new UserIndexViewModel
                {
                    UserID = item.UserID,
                    UserName = item.UserName,
                    Email = item.Email,
                });
            }

            return ResultList;
        }

        private UserEditViewModel ToUserEditViewModel(User usr)
        {
            if (usr == null)
            {
                return null;
            }

            var Model = new UserEditViewModel
            {
                UserID = usr.UserID,
                UserName = usr.UserName,
                Email = usr.Email,
                Password = usr.Password,
                Roles = usr.Roles,
                AllRoles = _roleProvider.GetAllRoles()
                };
            

            return Model;
        }

        private User ToUser(UserCreateViewModel usr)
        {
            if (usr == null)
            {
                return null;
            }

            var Model = new User
            {
                UserID = usr.UserID,
                Email = usr.Email,
                Password = usr.Password,
                Roles = usr.Roles
            };


            return Model;
        }

        private User ToUser(UserEditViewModel usr)
        {
            if (usr == null)
            {
                return null;
            }

            var Model = new User
            {
                UserID = usr.UserID,
                Email = usr.Email,
                Password = usr.Password,
                Roles = usr.Roles
            };


            return Model;
        }



        #endregion
    }
}