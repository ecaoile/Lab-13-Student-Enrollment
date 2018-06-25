using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using StudentEnrollment.Models;

namespace StudentEnrollment.Models
{
    public class Student
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Level Level { get; set; }

        [Required, Display(Name = "Enrollment Term")]
        public EnrollmentTerm EnrollmentTerm { get; set; }

        //[Required, Display(Name = "Enrolled Course")]
        //public CourseName CourseName { get; set; }

        public Course Course { get; set; }

        public int CourseID { get; set; }
    }

    public enum Level
    {
        Undergraduate,
        Graduate
    }

    public enum EnrollmentTerm
    {
        [Display(Name = "Summer 2018")] Summer2018 = 1,
        [Display(Name = "Fall 2018")] Fall2018,
        [Display(Name = "Winter 2018")] Winter2018,
        [Display(Name = "Spring 2019")] Spring2019
    }

    //public enum CourseName
    //{
    //    [Display(Name = "C#")] CSharp = 1,
    //    Java,
    //    JavaScript,
    //    Python,
    //    [Display(Name = "Advanced Anger Management")]AngerMgmt,
    //    Biology,
    //    Psychology,
    //    Calculus
    //}
}
