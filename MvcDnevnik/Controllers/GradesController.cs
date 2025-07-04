using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace MvcDnevnik.Controllers
{
    public class GradesController : Controller
    {

        public IActionResult Index(string name)
        {
            ViewData["Message"] = name;

            return View();
        }

        
    }
}
