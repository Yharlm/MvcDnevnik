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
      
        public IActionResult Student()
        {
            ViewData["UserID"] = HttpContext.Session.GetObject<int>("UserID");
            return View();
        }
        
        public IActionResult Logged()
        {
            foreach (var user in _context.User)
            {
                if (user.Role != UserRole.Teacher)
                {
                   return RedirectToAction("Student", "Home");
                }

            }
            if (HttpContext.Session.GetObject<string>("CurrentUser") == "Admin@mail.com")
            {
                ViewData["UserAcces"] = "Admin";
                
            }
            ViewData["UserName"] = HttpContext.Session.GetObject<string>("CurrentUser");
            if (HttpContext.Session.GetObject<string>("CurrentUser") == null)
            {
                
                return RedirectToRoute("Login");
            }
            var Cookies = new Cookies.Cookie(Response.Cookies);
            Cookies.SetSomething_IDK(new List<string>()
            {
                HttpContext.Session.GetObject<string>("CurrentUser"),
                HttpContext.Session.GetObject<string>("Password")
            });


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
            var Cookies = new Cookies.Cookie(Response.Cookies);
            Cookies.Clear();
            return RedirectToAction("Index", "Home");
        }
        
        public IActionResult Index()
        {
            ViewData["UserName"] = HttpContext.Session.GetString("CurrentUser");
            var Cookies = new Cookies.Cookie(Request.Cookies);
            if (Cookies.GetWords().Length > 0)
            {
                User user = new User()
                {
                    Email = Cookies.GetWords()[0],
                    Password = Cookies.GetWords()[1]
                    
                };
                HttpContext.Session.SetObject("CurrentUser", user.Email);
                HttpContext.Session.SetObject("Password", user.Password);
                

            }
            foreach (var user in _context.User)
            {
                
                if (user.Email == HttpContext.Session.GetObject<string>("CurrentUser") && user.Password == HttpContext.Session.GetObject<string>("Password"))
                {
                    HttpContext.Session.SetObject("CurrentUser", user.Email);
                    HttpContext.Session.SetObject("Password", user.Password);
                    HttpContext.Session.SetObject("UserID", user.ID);
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

        public IActionResult Nuke() 
        {
            User admin = new User()
            {
                
                Email = "Admin@mail.com",
                Name = "Admin",
                Password = "Admin",
                Role = UserRole.Teacher,

                PhoneNumber = "0000000000",
                Temp = "0000"
            };
            
            HttpContext.Session.Clear();
            var Cookies = new Cookies.Cookie(Response.Cookies);
            Cookies.Clear();
            _context.Subject.RemoveRange(_context.Subject);
            _context.Student.RemoveRange(_context.Student);
            _context.Grade.RemoveRange(_context.Grade);
            _context.User.RemoveRange(_context.User);
            _context.User.Add(admin);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
