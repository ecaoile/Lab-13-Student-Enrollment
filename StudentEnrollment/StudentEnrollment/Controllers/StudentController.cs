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
        public readonly SchoolDbContext _context;

        public StudentController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(string courseName, string searchString)
        {
            ViewData["Courses"] = await _context.Courses.Select(c => c).ToListAsync();
            // Use LINQ to get list of students.
            IQueryable<string> courseQuery = from c in _context.Students
                                            orderby c.Course.Name
                                            select c.Course.Name;

            var students = from m in _context.Students
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(courseName))
            {
                students = students.Where(x => x.Course.Name == courseName);
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
        public async Task<IActionResult> Create()
        {
            ViewData["Courses"] = await _context.Courses.Select(c => c).ToListAsync();
            //var courseListingVM = await _context.Courses.Select(c => c).ToListAsync();

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
            ViewData["Courses"] = await _context.Courses.Select(c => c).ToListAsync();

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

        // GET: Student/Edit/id#
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Courses"] = await _context.Courses.Select(c => c).ToListAsync();
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

        // POST: Student/Edit/id#
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Level,EnrollmentTerm,CourseID")] Student student)
        {
            if (id != student.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.ID))
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
