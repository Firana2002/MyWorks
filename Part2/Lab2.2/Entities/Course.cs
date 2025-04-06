using System.Collections.Generic;

namespace Lab2._2.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public string? Description { get; set; }
        
        // Навигационные свойства
        // Добавляем свойство CourseTeachers
        public List<CourseTeacher> CourseTeachers { get; set; } = new();
        public List<CourseStudent> CourseStudents { get; set; } = new();
    }
}