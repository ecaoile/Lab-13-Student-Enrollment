using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Controllers;
using StudentEnrollment.Data;
using StudentEnrollment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XUnitTestProject2
{
    public class CourseControllerTests
    {
        /// <summary>
        /// tests for the following: 2. Create a Course
        /// </summary>
        [Fact]
        public async void CanAddCourse()
        {
            DbContextOptions<SchoolDbContext> options = new DbContextOptionsBuilder<SchoolDbContext>()
                                                            .UseInMemoryDatabase("CreateCourseDb").Options;
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

                CourseController testCC = new CourseController(context);

                // act
                await testCC.Create(course1);
                await testCC.Create(course2);


                var results1 = context.Courses.Where(c => c.Name == "Advanced Anger Management");
                var results2 = context.Courses.Where(c => c.Name == "C# and ASP.NET");

                // assert there is one instance of the results
                Assert.Equal(1, results1.Count());
                Assert.Equal(1, results2.Count());
            }
        }

        /// <summary>
        /// tests for the following: 4. Update a Course
        /// </summary>
        [Fact]
        public async void CanUpdateCourse()
        {
            DbContextOptions<SchoolDbContext> options = new DbContextOptionsBuilder<SchoolDbContext>()
                                                        .UseInMemoryDatabase("CourseUpdateTestDb")
                                                        .Options;

            using (SchoolDbContext context = new SchoolDbContext(options))
            {
                Course course1 = new Course();
                course1.ID = 42;
                course1.Name = "Advanced Anger Management";
                course1.Teacher = "Bob Saget";
                course1.CourseTerm = CourseTerm.Fall2018;

                CourseController testCC = new CourseController(context);

                var x = await testCC.Create(course1);

                course1.Name = "Making Unit Tests";
                course1.Teacher = "Ron Testmaster";
                course1.CourseTerm = CourseTerm.Winter2018;

                var update1 = await testCC.Edit(42, course1);
                var result1 = context.Courses.Where(a => a.Name == "Making Unit Tests");
                var result2 = context.Courses.Where(a => a.Name == "Advanced Anger Management");

                //Assert
                Assert.IsAssignableFrom<IActionResult>(x);
                Assert.Equal(1, result1.Count());
                Assert.Equal(0, result2.Count());
            }
        }

        /// <summary>
        /// tests for the following: 5. Delete a Course
        /// </summary>
        [Fact]
        public async void CanDeleteCourse()
        {
            DbContextOptions<SchoolDbContext> options = new DbContextOptionsBuilder<SchoolDbContext>()
                                            .UseInMemoryDatabase("CourseDeleteTestDb")
                                            .Options;
            using (SchoolDbContext context = new SchoolDbContext(options))
            {
                // arrange
                Course course1 = new Course();
                course1.ID = 55;
                course1.Name = "Advanced Anger Management";
                course1.Teacher = "Bob Saget";
                course1.CourseTerm = CourseTerm.Fall2018;

                Course course2 = new Course();
                course2.ID = 66;
                course2.Name = "Making Unit Tests";
                course2.Teacher = "Ron Testmaster";
                course2.CourseTerm = CourseTerm.Winter2018;

                CourseController testCC = new CourseController(context);

                // act
                await testCC.Create(course1);
                await testCC.Create(course2);

                var x = await testCC.Delete(55);
                var y = await testCC.DeleteConfirmed(55);

                var result1 = context.Courses.Where(a => a.Name == "Advanced Anger Management");
                var result2 = context.Courses.Where(a => a.Name == "Making Unit Tests");

                //Assert
                Assert.IsAssignableFrom<IActionResult>(x);
                Assert.Equal(0, result1.Count());
                Assert.Equal(1, result2.Count());
            }
        }

        /// <summary>
        /// tests for part of 7. Getters and Setters for all model properties
        /// </summary>
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

        /// <summary>
        /// tests for part of 7. Getters and Setters for all model properties
        /// </summary>
        [Fact]
        public void CourseListingVMTest()
        {
            // arrange
            Course course1 = new Course();
            course1.ID = 77;
            course1.Name = "Advanced Anger Management";
            course1.Teacher = "Bob Saget";
            course1.CourseTerm = CourseTerm.Fall2018;

            Course course2 = new Course();
            course2.ID = 88;
            course2.Name = "Making Unit Tests";
            course2.Teacher = "Ron Testmaster";
            course2.CourseTerm = CourseTerm.Winter2018;

            CourseListingViewModel courseLVM = new CourseListingViewModel();

            List<Course> demCourses = new List<Course>
            {
                course1,
                course2
            };

            courseLVM.Courses = demCourses;
            courseLVM.Course = course2;

            var result1 = courseLVM.Courses.FirstOrDefault(c => c.ID == 77);
            var result2 = courseLVM.Courses.FirstOrDefault(c => c.ID == 88);

            Assert.Equal(course1, result1);
            Assert.Equal(course2, result2);
            Assert.Equal(2, courseLVM.Courses.Count());
            Assert.Equal(course2, courseLVM.Course);
        }

        /// <summary>
        /// tests for part of 7. Getters and Setters for all model properties
        /// </summary>
        [Fact]
        public async void CourseDetailVMTest()
        {
            DbContextOptions<SchoolDbContext> options = new DbContextOptionsBuilder<SchoolDbContext>()
                                 .UseInMemoryDatabase("CourseDetailVMDb")
                                 .Options;
            using (SchoolDbContext context = new SchoolDbContext(options))
            {
                // arrange
                Student student1 = new Student();
                student1.ID = 4;
                student1.Name = "Bill Test";
                student1.Level = Level.Graduate;
                student1.CourseID = 88;
                student1.EnrollmentTerm = EnrollmentTerm.Fall2018;

                Student student2 = new Student();
                student2.ID = 5;
                student2.Name = "Sally Testing";
                student2.Level = Level.Graduate;
                student2.CourseID = 88;
                student2.EnrollmentTerm = EnrollmentTerm.Spring2019;

                Course course1 = new Course();
                course1.ID = 77;
                course1.Name = "Advanced Anger Management";
                course1.Teacher = "Bob Saget";
                course1.CourseTerm = CourseTerm.Fall2018;

                Course course2 = new Course();
                course2.ID = 88;
                course2.Name = "Making Unit Tests";
                course2.Teacher = "Ron Testmaster";
                course2.CourseTerm = CourseTerm.Spring2019;

                // act
                await context.Students.AddAsync(student1);
                await context.Students.AddAsync(student2);
                await context.Courses.AddAsync(course1);
                await context.Courses.AddAsync(course2);
                await context.SaveChangesAsync();

                CourseDetailViewModel courseDVM1 = await CourseDetailViewModel.FromIDAsync(77, context);
                CourseDetailViewModel courseDVM2 = await CourseDetailViewModel.FromIDAsync(88, context);

                var result1 = courseDVM1.Course.ID;
                var result2 = courseDVM2.Course.ID;

                // assert
                Assert.True(courseDVM1.Students.Count() == 0);
                Assert.Equal(2, courseDVM2.Students.Count());
                Assert.Equal(77, result1);
                Assert.Equal(88, result2);
            }
        }
    }
}
