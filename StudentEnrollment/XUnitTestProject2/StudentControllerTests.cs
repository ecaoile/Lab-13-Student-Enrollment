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
    public class StudentControllerTests
    {
        /// <summary>
        /// tests for the following functionality: 1. Create a Student
        /// </summary>
        [Fact]
        public async void CanCreateStudent()
        {
            DbContextOptions<SchoolDbContext> options = new DbContextOptionsBuilder<SchoolDbContext>()
                                                            .UseInMemoryDatabase("CreateStudentDB").Options;
            using (SchoolDbContext context = new SchoolDbContext(options))
            {
                // arrange
                Student student1 = new Student();
                student1.Name = "Bill Test";
                student1.Level = Level.Undergraduate;
                student1.CourseID = 1;
                student1.EnrollmentTerm = EnrollmentTerm.Summer2018;

                // act
                StudentController testSC = new StudentController(context);
                await testSC.Create(student1);

                var results1 = context.Students.Where(s => s.Name == "Bill Test");

                // assert there is one instance of the results
                Assert.Equal(1, results1.Count());
            }
        }

        /// <summary>
        /// tests for the following functionality: 3. Update a Student
        /// </summary>
        [Fact]
        public async void CanUpdateStudent()
        {

            DbContextOptions<SchoolDbContext> options = new DbContextOptionsBuilder<SchoolDbContext>()
                                            .UseInMemoryDatabase("StudentUpdateTestDb")
                                            .Options;

            using (SchoolDbContext context = new SchoolDbContext(options))
            {
                Student student1 = new Student();
                student1.ID = 3;
                student1.Name = "Bill Test";
                student1.Level = Level.Undergraduate;
                student1.CourseID = 1;
                student1.EnrollmentTerm = EnrollmentTerm.Summer2018;

                StudentController testSC = new StudentController(context);

                var create1 = await testSC.Create(student1);

                student1.ID = 3;
                student1.Name = "William Tester";
                student1.Level = Level.Graduate;
                student1.CourseID = 2;
                student1.EnrollmentTerm = EnrollmentTerm.Summer2018;

                var edited1 = await testSC.Edit(student1.ID, student1);
            
                var result1 = await context.Students.FirstOrDefaultAsync(s => s.Name == "Bill Test");
                var result2 = await context.Students.FirstOrDefaultAsync(s => s.Name == "William Tester");

                var result3 = testSC.Details(3);

                //Assert
                Assert.Null(result1);
                Assert.NotNull(result2);
                Assert.Equal("William Tester", student1.Name);
                Assert.Equal(Level.Graduate, student1.Level);
                Assert.Equal(2, student1.CourseID);
                Assert.Equal(EnrollmentTerm.Summer2018, student1.EnrollmentTerm);
            }
        }

        [Fact]
        public async void CanDeleteStudent()
        {
            DbContextOptions<SchoolDbContext> options = new DbContextOptionsBuilder<SchoolDbContext>()
                                                .UseInMemoryDatabase("DeleteStudentDB").Options;
            using (SchoolDbContext context = new SchoolDbContext(options))
            {
                // arrange
                Student student1 = new Student();
                student1.ID = 4;
                student1.Name = "Bill Test";
                student1.Level = Level.Undergraduate;
                student1.CourseID = 1;
                student1.EnrollmentTerm = EnrollmentTerm.Summer2018;

                Student student2 = new Student();
                student2.ID = 5;
                student2.Name = "Sally Testing";
                student2.Level = Level.Graduate;
                student2.CourseID = 3;
                student2.EnrollmentTerm = EnrollmentTerm.Spring2019;

                // act
                StudentController testSC = new StudentController(context);
                await testSC.Create(student1);
                await testSC.Create(student2);
                await testSC.DeleteConfirmed(4);

                var results1 = context.Students.Where(s => s.Name == "Bill Test");
                var results2 = context.Students.Where(s => s.Name == "Sally Testing");

                // assert 
                Assert.Equal(0, results1.Count());
                Assert.Equal(1, results2.Count());
            }
        }

        /// <summary>
        /// tests for part of 7. Getters and Setters for all model properties
        /// </summary>
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

        /// <summary>
        /// tests for part of 7. Getters and Setters for all model properties
        /// </summary>
        [Fact]
        public void StudentListingVMTest()
        {
            // arrange
            Student student1 = new Student();
            student1.ID = 4;
            student1.Name = "Bill Test";
            student1.Level = Level.Undergraduate;
            student1.CourseID = 1;
            student1.EnrollmentTerm = EnrollmentTerm.Summer2018;

            Student student2 = new Student();
            student2.ID = 5;
            student2.Name = "Sally Testing";
            student2.Level = Level.Graduate;
            student2.CourseID = 3;
            student2.EnrollmentTerm = EnrollmentTerm.Spring2019;

            StudentListingViewModel studentLVM = new StudentListingViewModel();

            List<Student> demStudents = new List<Student>();
            demStudents.Add(student1);
            demStudents.Add(student2);
            studentLVM.students = demStudents;

            List<SelectListItem> Courses = new List<SelectListItem>();
            SelectListItem testSelectItem1 = new SelectListItem() { Text = "Biology", Value = "BIO" };
            Courses.Add(testSelectItem1);
            SelectListItem testSelectItem2 = new SelectListItem() { Text = "Psychology", Value = "PSY" };
            Courses.Add(testSelectItem2);

            SelectList datSelectList = new SelectList(Courses);
            studentLVM.courses = datSelectList;

            studentLVM.courseName = "Biology";

            var result1 = studentLVM.students.FirstOrDefault(s => s.ID == 4);
            var result2 = studentLVM.students.FirstOrDefault(s => s.ID == 5);

            var result3 = studentLVM.courses.ToList();

            Assert.Equal(student1, result1);
            Assert.Equal(student2, result2);
            Assert.Equal(2, result3.Count());
            Assert.Equal("Biology", studentLVM.courseName);
        }
    }
}
