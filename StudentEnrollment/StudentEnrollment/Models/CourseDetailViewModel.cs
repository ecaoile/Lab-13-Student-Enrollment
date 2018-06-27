using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StudentEnrollment.Models
{
    public class CourseDetailViewModel
    {
        public IEnumerable<Student> Students { get; set; }
        public Course Course { get; set; }
        public static async Task<CourseDetailViewModel> FromIDAsync(int id, SchoolDbContext context)
        {
            CourseDetailViewModel cvm = new CourseDetailViewModel();


            cvm.Course = await context.Courses.Where(c => c.ID == id).SingleAsync();

            cvm.Students = await context.Students.Where(s => s.CourseID == cvm.Course.ID)
                    .Select(s => s).ToListAsync();

            return cvm;
        }
    }
}
