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

        public string name { get; set; }

        public List<Student> students;
    }

    //public enum Name
    //{
    //    [Display(Name = "C# and ASP.NET Core")]CSharp,
    //    Java,
    //    JavaScript,
    //    Python
    //}
}
