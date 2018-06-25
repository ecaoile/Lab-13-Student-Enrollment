using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Data;
using StudentEnrollment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Controllers
{
    public class StudentController : Controller
    {
        public readonly StudentDbContext _context;

        public StudentController(StudentDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(CourseName courseName, string searchString)
        {
            // Use LINQ to get list of students.
            IQueryable<CourseName> courseQuery = from c in _context.Students
                                            orderby c.CourseName
                                            select c.CourseName;

            var students = from m in _context.Students
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString));
            }

            if ((int)courseName != 0)
            {
                students = students.Where(x => x.CourseName == courseName);
            }

            var studentListingVM = new StudentListingViewModel();
            studentListingVM.courses = new SelectList(await courseQuery.Distinct().ToListAsync());
            studentListingVM.students = await students.ToListAsync();

            return View(studentListingVM);
        }

        /// <summary>
        /// get: create student
        /// </summary>
        /// <returns>view</returns>
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Student/Details/
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // GET: Student/Delete/id#
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var student = await _context.Students.
                FirstOrDefaultAsync(s => s.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/id#
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(s => s.ID == id);
        }
    }
}
