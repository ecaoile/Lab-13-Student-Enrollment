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
    public class CourseController : Controller
    {
        public readonly SchoolDbContext _context;

        public CourseController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index(string courseName, string searchString)
        {
            // Use LINQ to get list of students.
            IQueryable<string> courseQuery = from c in _context.Students
                                             orderby c.Course.Name
                                             select c.Course.Name;

            var courses = from m in _context.Courses
                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(s => s.Name.Contains(searchString));
            }

            var courseListingVM = new CourseListingViewModel();
            courseListingVM.Courses = await courses.ToListAsync();

            return View(courseListingVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Course/Edit/id#
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Course/Edit/id#
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Teacher,CourseTerm")] Course course)
        {
            if (id != course.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.ID))
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
            return View(course);
        }

        // GET: Course/Details/
        public async Task<IActionResult> Details(int? id)
        {
            //ViewData["Courses"] = await _context.Courses.Select(c => c).ToListAsync();
            //ViewData["Students"] = await _context.Students.Select(s => s).ToListAsync();

            if (id.HasValue)
            {
                return View(await CourseDetailViewModel.FromIDAsync(id.Value, _context));
            }
            else
            {
                return RedirectToAction("Index");
            }

            //var courseDetails = _context.CourseDetails.FromIDAsync(id);
            //var course = await _context.Courses
            //    .FirstOrDefaultAsync(m => m.ID == id);

            
            //if (course == null)
            //{
            //    return NotFound();
            //}

            //return View(course);
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(s => s.ID == id);
        }
    }
}
