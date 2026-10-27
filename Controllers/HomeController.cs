using Microsoft.AspNetCore.Mvc;
using para.Models;
using System.Diagnostics;

namespace para.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Product");
        }

        [Route("Home/Error/{statusCode?}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode)
        {
            if (statusCode.HasValue)
            {
                if (statusCode == 404)
                {
                    ViewBag.ErrorMessage = "Запрашиваемая страница не найдена.";
                }
                else if (statusCode == 500)
                {
                    ViewBag.ErrorMessage = "Внутренняя ошибка сервера.";
                }
                else
                {
                    ViewBag.ErrorMessage = $"Код ошибки: {statusCode}";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Произошла непредвиденная ошибка в приложении.";
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}