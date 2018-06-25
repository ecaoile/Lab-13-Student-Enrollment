using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models
{
    public class StudentListingViewModel
    {
        public List<Student> students;
        public SelectList courses;
        public CourseName courseName { get; set; }
    }
}
