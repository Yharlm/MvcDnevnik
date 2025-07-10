using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MvcDnevnik.Data;
using MvcDnevnik.Models;
using SessionExtensions = MvcDnevnik.Models.SessionExtensions;

namespace MvcDnevnik.Controllers
{
    public class LoginController : Controller
    {
        private readonly MvcDnevnikContext _context;

        public LoginController(MvcDnevnikContext context)
        {
            _context = context;
        }



        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(User user)
        {


            foreach (var u in _context.User)
            {
                if (u.Email == user.Email && u.Password == user.Password)
                {
                    HttpContext.Session.SetObject("CurrentUser", u.Email);
                    HttpContext.Session.SetObject("Password", u.Password);
                    return RedirectToAction("Index", "Home");
                }
            }

            
            return View() ;
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp([Bind("ID,Email,Name,Password,Role")] User user)
        {
            foreach (var u in _context.User)
            {
                if (u.Email == user.Email || u.Name == user.Name)
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                    return View();
                }
            }
            user.PhoneNumber = "0000000000"; // Default phone number
            user.Temp = "0000"; // Default temporary code
            
            _context.Add(user);
            _context.SaveChanges();
            HttpContext.Session.SetObject("CurrentUser", user.Email);
            HttpContext.Session.SetObject("Password", user.Password);
            return RedirectToAction("Index", "Home");
        }
        
        
    }
}
