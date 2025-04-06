using Lab2._2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Lab2._2.Data
{
    public class ApplicationContext : DbContext
    {
        // Добавьте все необходимые DbSet свойства
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<CourseTeacher> CourseTeachers { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка связи многие-ко-многим для Course и Teacher
            modelBuilder.Entity<CourseTeacher>()
                .HasKey(ct => new { ct.CourseId, ct.TeacherId });
            
            modelBuilder.Entity<CourseTeacher>()
                .HasOne(ct => ct.Course)
                .WithMany(c => c.CourseTeachers)
                .HasForeignKey(ct => ct.CourseId);
            
            modelBuilder.Entity<CourseTeacher>()
                .HasOne(ct => ct.Teacher)
                .WithMany(t => t.CourseTeachers)
                .HasForeignKey(ct => ct.TeacherId);
                
            // Настройка связи многие-ко-многим для Course и Student
            modelBuilder.Entity<CourseStudent>()
                .HasKey(cs => new { cs.CourseId, cs.StudentId });
            
            modelBuilder.Entity<CourseStudent>()
                .HasOne(cs => cs.Course)
                .WithMany(c => c.CourseStudents)
                .HasForeignKey(cs => cs.CourseId);
            
            modelBuilder.Entity<CourseStudent>()
                .HasOne(cs => cs.Student)
                .WithMany(s => s.CourseStudents)
                .HasForeignKey(cs => cs.StudentId);
        }
    }
}