using CoreMVC_CustomMiddleware_Serilog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoreMVC_CustomMiddleware_Serilog.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            int a = 10, b = 0, c;
            c = a / b;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
