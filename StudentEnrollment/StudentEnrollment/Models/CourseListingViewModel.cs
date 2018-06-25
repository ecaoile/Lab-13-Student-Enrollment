using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models
{
    public class CourseListingViewModel
    {
        public IEnumerable<Student> Students { get; set; }
        public Course Course { get; set; }
        public List<Course> Courses { get; set; }
        public static async Task<CourseListingViewModel> FromIDAsync(int id, SchoolDbContext context)
        {
            CourseListingViewModel cvm = new CourseListingViewModel();

            cvm.Course = await context.Courses.Where(c => c.ID == id).SingleAsync();

            cvm.Students = await context.Students.Where(s => s.Course == cvm.Course)
                                                .Select(s => s)
                                                .ToListAsync();
            return cvm;
        }
    }
}
