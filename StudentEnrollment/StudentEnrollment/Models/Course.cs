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

        public string Teacher { get; set; }

        public CourseTerm CourseTerm { get; set; }

        public List<Student> students;
    }

    public enum CourseTerm
    {
        Summer2018 = 1,
        Fall2018,
        Winter2018,
        Spring2019
    }
}
