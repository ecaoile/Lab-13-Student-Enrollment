using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Data;
using StudentEnrollment.Models;
using System;
using System.Linq;
using Xunit;

namespace XUnitTestProject2
{
    public class UnitTest1
    {
        [Fact]
        public async void CanSaveToDatabase()
        {
            DbContextOptions<SchoolDbContext> options = new DbContextOptionsBuilder<SchoolDbContext>()
                                                            .UseInMemoryDatabase("DbCanSave").Options;
            using (SchoolDbContext context = new SchoolDbContext(options))
            {
                // arrange
                Course course = new Course();
                course.Name = "Advanced Anger Management";
                course.Teacher = "Bob Saget";
                course.CourseTerm = CourseTerm.Fall2018;

                // act
                await context.Courses.AddAsync(course);
                await context.SaveChangesAsync();

                var results = context.Courses.Where(c => c.Name == "Advanced Anger Management");
                
                // assert there is one instance of the results
                Assert.Equal(1, results.Count());
            }
        }

        [Fact]
        public async void CanCreate()
        {

        }
    }
}
