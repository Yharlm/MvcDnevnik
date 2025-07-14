using System;
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
    public class ComplaintsController : Controller
    {
        private readonly MvcDnevnikContext _context;

        public ComplaintsController(MvcDnevnikContext context)
        {
            _context = context;
        }

        // GET: Complaints
        public async Task<IActionResult> Index()
        {
            return View(await _context.Complaints.ToListAsync());
        }

        // GET: Complaints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaints = await _context.Complaints
                .FirstOrDefaultAsync(m => m.ID == id);
            if (complaints == null)
            {
                return NotFound();
            }

            return View(complaints);
        }

        // GET: Complaints/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Complaints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Date,ComplaintText,Status")] Complaints complaints)
        {
            if (ModelState.IsValid)
            {
                _context.Add(complaints);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(complaints);
        }

        // GET: Complaints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaints = await _context.Complaints.FindAsync(id);
            if (complaints == null)
            {
                return NotFound();
            }
            return View(complaints);
        }

        // POST: Complaints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Date,ComplaintText,Status")] Complaints complaints)
        {
            if (id != complaints.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaints);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintsExists(complaints.ID))
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
            return View(complaints);
        }

        // GET: Complaints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaints = await _context.Complaints
                .FirstOrDefaultAsync(m => m.ID == id);
            if (complaints == null)
            {
                return NotFound();
            }

            return View(complaints);
        }

        // POST: Complaints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complaints = await _context.Complaints.FindAsync(id);
            if (complaints != null)
            {
                _context.Complaints.Remove(complaints);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintsExists(int id)
        {
            return _context.Complaints.Any(e => e.ID == id);
        }

        public IActionResult Remarks(int ID)
        {
            List<Complaint_Subject> complaint_Subject = new List<Complaint_Subject>();
                
            foreach(var sub in _context.Subject)
            {
                Complaint_Subject comp = new Complaint_Subject();
                comp.Subject = sub;
                foreach(var com in from c in _context.Complaints where c.Subject.ID == sub.ID where c.Student.User_ID_Student == ID select c)
                {
                    comp.Complaints.Add(com);
                }
                complaint_Subject.Add(comp);
            }

            
            return View(complaint_Subject);
        }
    }
}
