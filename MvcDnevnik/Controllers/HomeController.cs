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
        private readonly MvcDnevnikContext _context;
        public HomeController(ILogger<HomeController> logger, MvcDnevnikContext context)
        {
            _logger = logger;
            _context = context;
        }
      
        
        
        public IActionResult Logged()
        {
            ViewData["UserName"] = HttpContext.Session.GetObject<string>("CurrentUser");
            if (HttpContext.Session.GetObject<string>("CurrentUser") == null)
            {
                
                return RedirectToRoute("Login");
            }
            //var Cokies = new Cookies.Cookie(Request.Cookies);

            List<string> login =new List<string>()
            {
                HttpContext.Session.GetObject<string>("CurrentUser"),
                HttpContext.Session.GetObject<string>("Password")
            };
            
            //Cokies.SetSomething_IDK(login);
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        
        public IActionResult Index()
        {
            ViewData["UserName"] = HttpContext.Session.GetString("CurrentUser");
            //var Cokies = new Cookies.Cookie(Request.Cookies);
            
            foreach(var user in _context.User)
            {
                if (user.Email == HttpContext.Session.GetString("CurrentUser") && user.Password == HttpContext.Session.GetString("Password"))
                {
                    HttpContext.Session.SetObject("CurrentUser", user.Email);
                    HttpContext.Session.SetObject("Password", user.Password);
                    return RedirectToAction("Logged");
                }
            }
            
            
            


            if (HttpContext.Session.GetString("CurrentUser") != null)
            {
                return RedirectToAction("Login", "Login");
            }

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
