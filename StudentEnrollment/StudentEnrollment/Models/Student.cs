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
        [Display(Name = "Course")]
        public CourseName CourseName { get; set; }
    }

    public enum CourseName
    {
        [Display(Name = "C#")] CSharp = 1,
        Java,
        JavaScript,
        Python
    }
}
