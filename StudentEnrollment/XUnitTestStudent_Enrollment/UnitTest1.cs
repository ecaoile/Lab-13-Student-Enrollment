using System;
using Xunit;
using StudentEnrollment;
using StudentEnrollment.Data;
using StudentEnrollment.Models;
using XUnitTestStudent_Enrollment;
using StudentEnrollment.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace XUnitTestStudent_Enrollment
{
    public class UnitTest1
    {
        [Fact]
        public void CourseControllerCanCreate()
        {
            DbContextOptions<SchoolDbContext> options = new DbContextOptions<SchoolDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using (SchoolDbContext context = new SchoolDbContext(options))
            {
                // Arrange
                Course course = new Course();
                course.Name = "Psychology";
                // other fields

            
            }
        }
    }
}
