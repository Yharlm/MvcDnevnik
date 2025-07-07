using Microsoft.AspNetCore.Mvc;
using MvcDnevnik.Data;

namespace MvcDnevnik.Controllers
{
    public class StudentsController : Controller
    {
        private readonly MvcDnevnikContext _context;

        public StudentsController(MvcDnevnikContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);

        }
    }
}
