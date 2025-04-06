using System.Collections.Generic;

namespace Lab2._2.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        
        // Навигационное свойство
        public List<CourseStudent> CourseStudents { get; set; } = new();
    }
}