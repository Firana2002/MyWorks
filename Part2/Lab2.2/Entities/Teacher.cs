using System.Collections.Generic;

namespace Lab2._2.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        
        // Навигационное свойство
        public List<CourseTeacher> CourseTeachers { get; set; } = new();
    }
}