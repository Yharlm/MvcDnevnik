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

        

        public async Task<IActionResult> Index(int id)
        {
            var user = _context.User.FirstOrDefault();
            user.Temp = id.ToString() + '/'; // Set a default value for Temp, if needed
            _context.Update(user);
            await _context.SaveChangesAsync();


            return View(context.Student);

        }

        // " asp-route-id="@item.ID" " - turns id into a route parameter(sub-page or Component?)
        public async Task<IActionResult> Select(int id)
        {
            var user = _context.User.FirstOrDefault();
            user.Temp = id.ToString() + '/'; // Set a default value for Temp, if needed
            _context.Update(user);
            await _context.SaveChangesAsync();

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
            var user = _context.User.FirstOrDefault();
            grade.Subject.ID = user.Temp.Split('/')[0];
            grade.Student.ID = user.Temp.Split('/')[1];


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
