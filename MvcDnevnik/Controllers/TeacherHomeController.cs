using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcDnevnik.Data;
using MvcDnevnik.Models;
using SessionExtensions = MvcDnevnik.Models.SessionExtensions;

namespace MvcDnevnik.Controllers
{
    public class TeacherHomeController(MvcDnevnikContext context) : Controller
    {
        


        private readonly MvcDnevnikContext _context = context;


        public IActionResult Index()
        {

            return View(_context.Subject);
        }

        public async Task<IActionResult> Subject(int Sub)
        {
            HttpContext.Session.SetObject<int>("SubjectID", Sub);



            var list = new List<Student_grade>();


            foreach (var s in _context.Student)
            {
                var grades = from g in _context.Grade
                             where g.Subject.ID == Sub
                             where g.Student.ID == s.ID
                             select g;
                list.Add(new Student_grade()
                {
                    Student = s,
                    Grades = grades.ToList()
                });

            }

            return View(list);

        }

        public IActionResult CreateRedirect(int Stu)
        {
            HttpContext.Session.SetObject<int>("StudentID", Stu);
            return RedirectToAction(nameof(Create));
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

            // Assuming Temp is formatted as "subjectId/studentId"
            int StudentID = HttpContext.Session.GetObject<int>("StudentID");
            int SubjectID = HttpContext.Session.GetObject<int>("SubjectID"); // Assuming Temp is formatted as "subjectId/studentId"

            grade.Subject = _context.Subject.FirstOrDefault(s => s.ID == SubjectID);
            grade.Student = _context.Student.FirstOrDefault(s => s.ID == StudentID);

            if (_context.Student.Contains(grade.Student) && _context.Subject.Contains(grade.Subject))
            {
                _context.Add(grade);
                await _context.SaveChangesAsync();
                return Redirect("/TeacherHome/Subject?Sub=" + SubjectID);

            }
            else
            {
                ModelState.AddModelError("", "Invalid Student or Subject");
                return View(grade);
            }




        }
    }
}
