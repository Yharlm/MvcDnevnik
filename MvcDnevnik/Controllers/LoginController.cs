using Microsoft.AspNetCore.Mvc;
using MvcDnevnik.Models;
using SessionExtensions = MvcDnevnik.Models.SessionExtensions;

namespace MvcDnevnik.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(User user)
        {
            
            
            //var Cookie = new Cookies.Cookie();
            //if (requestCookies.GetWords().Length > 0)
            //{
            //    responseCookies.Clear();
            //}
            HttpContext.Session.SetObject("CurrentUser", user.Email);
            HttpContext.Session.SetObject("Password", user.Password);

            return RedirectToAction("Logged", "Home");
        }
    }
}
