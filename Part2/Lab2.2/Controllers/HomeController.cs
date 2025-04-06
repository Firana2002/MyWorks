using Lab2._2.Data;
using Lab2._2.Entities;
using Lab2._2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Lab2._2.Models;
using Lab2._2.Models;
namespace Lab2._2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
            
            // Инициализация базы данных при необходимости
            if (_context.Database.EnsureCreated())
            {
                SeedData();
            }
        }

        public IActionResult Index()
        {
            // Eager Loading: Получаем все курсы с преподавателями и студентами
            var courses = _context.Courses
                .Include(c => c.CourseTeachers)
                    .ThenInclude(ct => ct.Teacher)
                .Include(c => c.CourseStudents)
                    .ThenInclude(cs => cs.Student)
                .ToList();

            var teachers = _context.Teachers
                .Include(t => t.CourseTeachers)
                    .ThenInclude(ct => ct.Course)
                .ToList();

            var students = _context.Students
                .Include(s => s.CourseStudents)
                    .ThenInclude(cs => cs.Course)
                .ToList();

            var viewModel = new CourseViewModel
            {
                Courses = courses,
                Teachers = teachers,
                Students = students
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Демонстрация Explicit Loading (явной загрузки)
        public IActionResult ExplicitLoadingDemo()
        {
            var course = _context.Courses.FirstOrDefault();
            if (course != null)
            {
                // Явная загрузка преподавателей для курса
                _context.Entry(course).Collection(c => c.CourseTeachers).Load();
                foreach (var ct in course.CourseTeachers)
                {
                    _context.Entry(ct).Reference(t => t.Teacher).Load();
                }

                // Явная загрузка студентов для курса
                _context.Entry(course).Collection(c => c.CourseStudents).Load();
                foreach (var cs in course.CourseStudents)
                {
                    _context.Entry(cs).Reference(s => s.Student).Load();
                }

                // Здесь можно передать данные в представление
                ViewBag.CourseName = course.Title;
                ViewBag.CourseTeachers = course.CourseTeachers.Select(ct => ct.Teacher).ToList();
                ViewBag.CourseStudents = course.CourseStudents.Select(cs => cs.Student).ToList();
            }

            return View();
        }

        // Демонстрация работы с данными
        public IActionResult ManageData()
        {
            // Добавление нового курса
            var newCourse = new Course
            {
                Title = "Python для начинающих",
                Duration = 30,
                Description = "Введение в программирование на Python"
            };
            _context.Courses.Add(newCourse);
            _context.SaveChanges();

            // Обновление существующего курса
            var courseToUpdate = _context.Courses.FirstOrDefault(c => c.Title == "SQL Database");
            if (courseToUpdate != null)
            {
                courseToUpdate.Duration = 35;
                courseToUpdate.Description = "Расширенный курс по SQL и базам данных";
                _context.SaveChanges();
            }

            // Удаление курса (для примера)
            var courseToDelete = _context.Courses.FirstOrDefault(c => c.Title == "Python для начинающих");
            if (courseToDelete != null)
            {
                _context.Courses.Remove(courseToDelete);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // Метод для заполнения базы данных начальными значениями
        private void SeedData()
        {
            // Добавление курсов
            var csharpCourse = new Course { Title = "C# Programming", Duration = 40, Description = "Основы программирования на C#" };
            var javaCourse = new Course { Title = "Java Development", Duration = 45, Description = "Разработка на Java" };
            var sqlCourse = new Course { Title = "SQL Database", Duration = 30, Description = "Работа с базами данных SQL" };
            
            _context.Courses.AddRange(csharpCourse, javaCourse, sqlCourse);
            _context.SaveChanges();
            
            // Добавление преподавателей
            var teacher1 = new Teacher { FirstName = "Иван", LastName = "Петров", Email = "ivan@example.com" };
            var teacher2 = new Teacher { FirstName = "Мария", LastName = "Сидорова", Email = "maria@example.com" };
            
            _context.Teachers.AddRange(teacher1, teacher2);
            _context.SaveChanges();
            
            // Добавление студентов
            var student1 = new Student { FirstName = "Алексей", LastName = "Иванов", Age = 20, City = "Москва" };
            var student2 = new Student { FirstName = "Елена", LastName = "Смирнова", Age = 22, City = "Санкт-Петербург" };
            var student3 = new Student { FirstName = "Дмитрий", LastName = "Соколов", Age = 21, City = "Новосибирск" };
            
            _context.Students.AddRange(student1, student2, student3);
            _context.SaveChanges();
            
            // Установка связей преподаватель-курс
            _context.CourseTeachers.AddRange(
                new CourseTeacher { CourseId = csharpCourse.Id, TeacherId = teacher1.Id },
                new CourseTeacher { CourseId = javaCourse.Id, TeacherId = teacher1.Id },
                new CourseTeacher { CourseId = sqlCourse.Id, TeacherId = teacher2.Id }
            );
            
            // Установка связей студент-курс с оценками
            _context.CourseStudents.AddRange(
                new CourseStudent { CourseId = csharpCourse.Id, StudentId = student1.Id, Grade = 85 },
                new CourseStudent { CourseId = csharpCourse.Id, StudentId = student2.Id, Grade = 92 },
                new CourseStudent { CourseId = javaCourse.Id, StudentId = student1.Id, Grade = 78 },
                new CourseStudent { CourseId = javaCourse.Id, StudentId = student3.Id, Grade = 88 },
                new CourseStudent { CourseId = sqlCourse.Id, StudentId = student2.Id, Grade = 95 },
                new CourseStudent { CourseId = sqlCourse.Id, StudentId = student3.Id, Grade = 90 }
            );
            
            _context.SaveChanges();
            
            _logger.LogInformation("База данных заполнена начальными данными.");
        }
    }
}