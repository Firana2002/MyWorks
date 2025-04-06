using Lab2._2.Entities;
using System.Collections.Generic;

namespace Lab2._2.Models
{
    public class CourseViewModel
    {
        public List<Course> Courses { get; set; } = new List<Course>();
        public List<Teacher> Teachers { get; set; } = new List<Teacher>();
        public List<Student> Students { get; set; } = new List<Student>();
    }
}