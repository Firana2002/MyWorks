using Lab2._2.Entities;

namespace Lab2._2.Entities
{
    public class CourseStudent
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }
        
        public int StudentId { get; set; }
        public Student Student { get; set; }
        
        public int? Grade { get; set; }
    }
}