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

        public Level Level { get; set; }

        [Display(Name = "Enrollment Term")]
        public Term EnrollmentTerm { get; set; }

        [Required]
        [Display(Name = "Enrolled Course")]
        public CourseName CourseName { get; set; }
    }

    public enum Level
    {
        Undergraduate,
        Graduate
    }

    public enum Term
    {
        [Display(Name = "Summer 2018")] Summer2018 = 1,
        [Display(Name = "Fall 2018")] Fall2018,
        [Display(Name = "Winter 2018")] Winter2018,
        [Display(Name = "Spring 2019")] Spring2019
    }

    public enum CourseName
    {
        [Display(Name = "C#")] CSharp = 1,
        Java,
        JavaScript,
        Python
    }
}
