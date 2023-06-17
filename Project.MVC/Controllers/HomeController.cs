using Microsoft.AspNetCore.Mvc;
using Project.MVC.Models;
using System.Diagnostics;

namespace Project.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();


        }

        [HttpPost]
        public IActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
            {
                return View();
            }

            if (page.Equals("makes", System.StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Index", "VehicleMakes");
            }

            if (page.Equals("models", System.StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Index", "VehicleModels");
            }

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