using Microsoft.AspNetCore.Mvc;
using MvcDnevnik.Data;
using MvcDnevnik.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

namespace MvcDnevnik.Controllers
{
    public class TeacherHomeController(MvcDnevnikContext context) : Controller
    {
        private readonly MvcDnevnikContext _context = context;

        

        public IActionResult Index(int sub)
        {
            return View(context.Student);

        }

        // " asp-route-id="@item.ID" " - turns id into a route parameter(sub-page or Component?)
        public async Task<IActionResult> Select(int id)
        {
            ViewData["Student"] = _context.Student.Find(id);
            var list = from s in _context.Grade
                       where s.Student.ID == id
					   select s;

            
            return View(list);
        }

		public IActionResult Create()
		{
			return View();
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Value,Date,Description")] Grade grade)
		{
            
            if (ModelState.IsValid)
			{
                

                
				_context.Add(grade);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(grade);
		}
	}
}
