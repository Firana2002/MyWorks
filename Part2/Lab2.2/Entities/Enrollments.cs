namespace Lab2._2.Entities
{
    public class Enrollments
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int? Grade { get; set; }
    }
}