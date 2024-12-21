using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PROYECTO_SWI.Models;

namespace PROYECTO_SWI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index() //agregar a los controladores
        {
            var userName = HttpContext.Session.GetString("UserName");

            ViewData["Message"] = $"Bienvenido {userName}";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
