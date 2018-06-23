using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models
{
    public class Course
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public Term CourseTerm { get; set; }

        public List<Student> students;
    }

    public enum CourseTerm
    {
        Summer2018 = 1,
        Fall2018,
        Winter2018,
        Spring2019
    }

    //public enum CourseName
    //{
    //    [Display(Name = "C#")] CSharp = 1,
    //    Java,
    //    JavaScript,
    //    Python
    //}   
}
