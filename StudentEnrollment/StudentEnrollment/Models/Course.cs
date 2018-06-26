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
    }

    public enum CourseTerm
    {
        [Display(Name = "Summer 2018")] Summer2018 = 1,
        [Display(Name = "Fall 2018")] Fall2018,
        [Display(Name = "Winter 2018")] Winter2018,
        [Display(Name = "Spring 2019")] Spring2019
    }
}
