using Microsoft.EntityFrameworkCore;

var context = new AppDbContext();
context.Database.EnsureCreated();

// Добавление
context.Courses.Add(new Course 
{ 
    Title = "C# Basics", 
    Duration = 40, 
    Description = "Основы программирования на C#" 
});
context.SaveChanges();

// Чтение
var courses = context.Courses.ToList();
Console.WriteLine("Все курсы:");
foreach (var course in courses)
{
    Console.WriteLine($"{course.Id}. {course.Title} ({course.Duration} часов)");
}

// Обновление
var firstCourse = context.Courses.First();
firstCourse.Duration = 45;
context.SaveChanges();

// Удаление
var courseToDelete = context.Courses.Find(1);
if (courseToDelete != null)
{
    context.Courses.Remove(courseToDelete);
    context.SaveChanges();
}