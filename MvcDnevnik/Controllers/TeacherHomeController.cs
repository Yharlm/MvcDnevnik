using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcDnevnik.Data;
using MvcDnevnik.Models;
using System.Diagnostics;
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
           //HttpContext.Session.SetObject<int>("SubjectID", Sub);



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
            ViewData["SubjectID"] = Sub;
            
            return View(list);

        }
        
       

        public IActionResult Create(int Sub,int Stu)
        {
            ViewData["SubjectID"] = Sub;
            ViewData["StudentID"] = Stu;

            return View();
        }
        
        public IActionResult Complaint(int Sub, int Stu)
        {
            ViewData["SubjectID"] = Sub;
            ViewData["StudentID"] = Stu;

            return View();
        }

        [HttpPost]
        public IActionResult Complaint([Bind("ID,Status,ComplaintText,Date")] Complaints Complaint,int Sub, int Stu)
        {
            Complaint.Subject = _context.Subject.FirstOrDefault(x => x.ID == Sub);
            Complaint.Student = _context.Student.FirstOrDefault(x => x.ID == Stu);

            _context.Add(Complaint);
            _context.SaveChanges();

            
            return RedirectToAction("Subject","TeacherHome",null,$"Sub={Sub}");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Value,Date,Description,Type")] Grade grade,int Sub,int Stu)
        {
            var user = _context.User.FirstOrDefault();

            // Assuming Temp is formatted as "subjectId/studentId"
            
            grade.Subject = _context.Subject.FirstOrDefault(x => x.ID == Sub);
            grade.Student = _context.Student.FirstOrDefault(x => x.ID == Stu);

            if (grade.Value > 0 && grade.Value<=6)
            {
                if (_context.Student.Contains(grade.Student) && _context.Subject.Contains(grade.Subject))
                {
                    _context.Add(grade);
                    await _context.SaveChangesAsync();
                    return Redirect("/TeacherHome/Subject?Sub=" + Sub);

                }
                else
                {
                    ModelState.AddModelError("", "Invalid Student or Subject");
                    return View(grade);
                }
            }
            else
            {
                ModelState.AddModelError("", "Grade must be between 1 and 6");
                return View(grade);
            }

            
            




        }
    }
}
