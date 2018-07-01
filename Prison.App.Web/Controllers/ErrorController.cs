using Prison.App.Web.Models;
using System.Web.Mvc;

namespace Prison.App.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        public ViewResult ServerError()
        {
            Response.StatusCode = 500;
            return View();
        }

        public ViewResult Unauthorized()
        {
            Response.StatusCode = 401;
            return View();
        }

        public ViewResult CustomError(string entity, string message)
        {
            if (entity != null)
            {
                switch (entity)
                {
                    case "Employee":
                        message = "Чтобы удалить этого сотрудника сначала удалите все задержания в которых он участвует!";
                        break;
                    case "PlaceOfStay":
                        message = "Чтобы удалить это место содержания сначала удалите все задержания, где оно упоминается!";
                        break;
                    case "Position":
                        message = "Чтобы удалить эту должность сначала удалите всех сотрудников с этой должностью!";
                        break;
                    case "Status":
                        message = "Чтобы удалить этот статус сначала удалите задержанных с этим статусом!";
                        break;
                    case "Role":
                        message = "Чтобы удалить эту роль сначала удалите пользователей с этой ролью!";
                        break;
                    default:
                        message = "Нарушено ограничение целостности связанных таблиц!";
                        break;
                }
            }

            return View("CustomError", new ErrorViewModel { Message = message });
        }
    }
}