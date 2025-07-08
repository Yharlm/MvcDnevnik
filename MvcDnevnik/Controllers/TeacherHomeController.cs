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


        public async Task<IActionResult> Subject(int id)
        {
            var user = _context.User.FirstOrDefault();
            user.Temp = id.ToString() + '/'; // Set a default value for Temp, if needed
            _context.Update(user);
            await _context.SaveChangesAsync();


            return View(context.Subject);

        }
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
            if (user.Temp.Split('/').Length == 1)
            {
                user.Temp = user.Temp + '/' + id.ToString();
            }
            else
            {
                user.Temp = user.Temp.Split("/")[0] + '/' + id.ToString();
            }
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
            
            int subjectId = int.Parse(user.Temp.Split('/')[0]); // Assuming Temp is formatted as "subjectId/studentId"
            int studentId = int.Parse(user.Temp.Split('/')[1]); // Assuming Temp is formatted as "subjectId/studentId"
            user.Temp = ""; // Clear Temp after using it to avoid confusion in future requests
            _context.Update(user);
            grade.Subject = _context.Subject.FirstOrDefault(s => s.ID == subjectId);
            grade.Student = _context.Student.FirstOrDefault(s => s.ID == studentId);

            if(_context.Student.Contains(grade.Student) && _context.Subject.Contains(grade.Subject))
            {
                _context.Add(grade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));


            }
            else
            {
                ModelState.AddModelError("", "Invalid Student or Subject");
                return View(grade);
            }

            
			
			
		}
	}
}
