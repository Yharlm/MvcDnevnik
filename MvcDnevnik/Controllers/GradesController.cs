﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcDnevnik.Data;
using MvcDnevnik.Models;

namespace MvcDnevnik.Controllers
{
    public class GradesController : Controller
    {
        private readonly MvcDnevnikContext _context;

        public GradesController(MvcDnevnikContext context)
        {
            _context = context;
        }

<<<<<<< HEAD
=======

        public IActionResult Check()
        {
            return View();
        }
>>>>>>> 8822f4abcd7cb05dbba53f4a283da55bb9cd3478
        // GET: Grades
        public async Task<IActionResult> Index()
        {
            return View(await _context.Grade.ToListAsync());
        }

        // GET: Grades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grade
                .FirstOrDefaultAsync(m => m.ID == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // GET: Grades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Grades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Value,Date,Description")] Grade grade)
        {
<<<<<<< HEAD
            Request.Form.TryGetValue("Student_ID", out var student_ID);
            Request.Form.TryGetValue("Subject_ID", out var subject_ID);

            if (int.TryParse(student_ID, out int studentId) && int.TryParse(subject_ID, out int subjectId))
            {
                grade.Student = await _context.Student.FindAsync(studentId);
                grade.Subject = await _context.Subject.FindAsync(subjectId);
            }
=======
>>>>>>> 8822f4abcd7cb05dbba53f4a283da55bb9cd3478
            if (ModelState.IsValid)
            {
                _context.Add(grade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grade);
        }

<<<<<<< HEAD
        //// GET: Grades/Edit/5
=======
        // GET: Grades/Edit/5
>>>>>>> 8822f4abcd7cb05dbba53f4a283da55bb9cd3478
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grade.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }
            return View(grade);
        }

        // POST: Grades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD
=======



>>>>>>> 8822f4abcd7cb05dbba53f4a283da55bb9cd3478
        public async Task<IActionResult> Edit(int id, [Bind("ID,Value,Date,Description")] Grade grade)
        {
            if (id != grade.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeExists(grade.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(grade);
        }

        // GET: Grades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grade
                .FirstOrDefaultAsync(m => m.ID == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grade = await _context.Grade.FindAsync(id);
            if (grade != null)
            {
                _context.Grade.Remove(grade);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeExists(int id)
        {
            return _context.Grade.Any(e => e.ID == id);
        }
    }
}
