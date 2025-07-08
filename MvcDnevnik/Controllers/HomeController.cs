using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcDnevnik.Data;
using MvcDnevnik.Models;
using System.Diagnostics;

namespace MvcDnevnik.Controllers
{
    public class HomeController : Controller
    {

        public const string SessionKeyName = "_Name";
        public const string SessionKeyAge = "_Age";


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        
        public IActionResult Index()
        {

            var Cokies = new Cookies.Cookie(Request.Cookies);
            

            ViewData["Names"] = Cokies.GetWords();
            return View();
        }

        public IActionResult Privacy()
        {
            //return View();
            
            HttpContext.Session.SetString(SessionKeyName, "John Doe");
            

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
