using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Controllers;
using StudentEnrollment.Data;
using StudentEnrollment.Models;
using System;
using System.Linq;
using Xunit;

namespace XUnitTestProject2
{
    public class CourseControllerTests
    {
        [Fact]
        public async void CanDoCRUDonCourses()
        {
            DbContextOptions<SchoolDbContext> options = new DbContextOptionsBuilder<SchoolDbContext>()
                                                            .UseInMemoryDatabase("DbCanSave").Options;
            using (SchoolDbContext context = new SchoolDbContext(options))
            {
                // arrange
                Course course1 = new Course();
                course1.Name = "Advanced Anger Management";
                course1.Teacher = "Bob Saget";
                course1.CourseTerm = CourseTerm.Fall2018;

                Course course2 = new Course();
                course2.Name = "C# and ASP.NET";
                course2.Teacher = "Amanda Iverson";
                course2.CourseTerm = CourseTerm.Summer2018;

                // act
                await context.Courses.AddAsync(course1);
                await context.SaveChangesAsync();

                await context.Courses.AddAsync(course2);
                await context.SaveChangesAsync();


                var results1 = context.Courses.Where(c => c.Name == "Advanced Anger Management");
                var results2 = context.Courses.Where(c => c.Name == "C# and ASP.NET");

                // assert there is one instance of the results
                Assert.Equal(1, results1.Count());
                Assert.Equal(1, results2.Count());

                context.Courses.Remove(course1);
                await context.SaveChangesAsync();

                Assert.Equal(0, results1.Count());
                Assert.Equal(1, results2.Count());
            }
        }

        [Fact]
        public async void CanCRUDCourseController()
        {
            DbContextOptions<SchoolDbContext> options = new DbContextOptionsBuilder<SchoolDbContext>()
                                                        .UseInMemoryDatabase("CourseCreateTestDb")
                                                        .Options;

            using (SchoolDbContext context = new SchoolDbContext(options))
            {
                Course course1 = new Course();
                course1.Name = "Advanced Anger Management";
                course1.Teacher = "Bob Saget";
                course1.CourseTerm = CourseTerm.Fall2018;

                Course course2 = new Course();
                course2.Name = "Making Unit Tests";
                course2.Teacher = "Ron Testmaster";
                course2.CourseTerm = CourseTerm.Winter2018;

                CourseController datCC = new CourseController(context);

                var x = await datCC.Create(course2);

                var results = context.Courses.Where(a => a.Name == "Making Unit Tests");

                //Assert
                Assert.Equal(1, results.Count());
                Assert.IsAssignableFrom<IActionResult>(x);
            }
        }

        [Fact]
        public void CourseNameGetterTest()
        {
            //Arrange
            Course course = new Course();
            course.Name = "Making Unit Tests";
            course.Teacher = "Ron Testmaster";
            course.CourseTerm = CourseTerm.Winter2018;
            course.Teacher = "Ronald McTester";

            Assert.Equal("Making Unit Tests", course.Name);
            Assert.Equal("Ronald McTester", course.Teacher);
            Assert.Equal(CourseTerm.Winter2018, course.CourseTerm);
        }
    }
}
