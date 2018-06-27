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
    public class StudentControllerTests
    {
        [Fact]
        public async void CanCRUDonStudent()
        {
            DbContextOptions<SchoolDbContext> options = new DbContextOptionsBuilder<SchoolDbContext>()
                                                            .UseInMemoryDatabase("DbCanSave").Options;
            using (SchoolDbContext context = new SchoolDbContext(options))
            {
                // arrange
                Student student1 = new Student();
                student1.Name = "Bill Testee";
                student1.Level = Level.Undergraduate;
                student1.CourseID = 1;
                student1.EnrollmentTerm = EnrollmentTerm.Summer2018;

                // act
                await context.Students.AddAsync(student1);
                await context.SaveChangesAsync();

                var results1 = context.Students.Where(s => s.Name == "Bill Testee");

                // assert there is one instance of the results
                Assert.Equal(1, results1.Count());
            }
        }

        [Fact]
        public async void CanCRUDStudentController()
        {

            DbContextOptions<SchoolDbContext> options = new DbContextOptionsBuilder<SchoolDbContext>()
                                            .UseInMemoryDatabase("StudentCreateTestDb")
                                            .Options;

            using (SchoolDbContext context = new SchoolDbContext(options))
            {
                Student student1 = new Student();
                student1.Name = "Bill Testee";
                student1.Level = Level.Undergraduate;
                student1.CourseID = 1;
                student1.EnrollmentTerm = EnrollmentTerm.Summer2018;


                //Act
                StudentController datCC = new StudentController(context);

                var x = await datCC.Create(student1);

                var results = context.Students.Where(a => a.Name == "Bill Testee");

                //Assert
                Assert.Equal(1, results.Count());
                Assert.IsAssignableFrom<IActionResult>(x);
            }
        }

        [Fact]
        public void StudentNameGetterTest()
        {
            //Arrange
            Student student = new Student();
            student.Name = "Mike Testabee";
            student.CourseID = 4;
            student.EnrollmentTerm = EnrollmentTerm.Summer2018;
            student.Name = "Michael Testabest";

            Assert.Equal("Michael Testabest", student.Name);
            Assert.Equal(4, student.CourseID);
            Assert.Equal(EnrollmentTerm.Summer2018, student.EnrollmentTerm);
        }
    }
}
