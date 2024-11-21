using AnastasiaKiselevaKt_44_21.Database;
using AnastasiaKiselevaKt_44_21.Interfaces.StudentsInterfaces;
using AnastasiaKiselevaKt_44_21.Models;
using Microsoft.EntityFrameworkCore;
using AnastasiaKiselevaKt_44_21.Filters.StudentFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AnastasiaKiselevaKt_44_21.Tests
{
    public class StudentTests : IDisposable
    {
        private readonly DbContextOptions<StudentDbContext> _dbContextOptions;
        private readonly StudentDbContext _context;
        private readonly StudentService _studentService;

        public StudentTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<StudentDbContext>()
                .UseInMemoryDatabase(databaseName: "student_db_an")
                .Options;

            _context = new StudentDbContext(_dbContextOptions);
            _studentService = new StudentService(_context);

            SeedDatabase();
        }

        private void SeedDatabase() // база данных
        {
            var groups = new List<Group>
    {
        new Group { GroupName = "KT-44-21", GroupId = 1 },
        new Group { GroupName = "KT-43-21", GroupId = 2 },
        new Group { GroupName = "KT-42-21", GroupId = 3 },
        new Group { GroupName = "KT-41-21", GroupId = 4 }
    };

            _context.Set<Group>().AddRange(groups);

            var students = new List<Student>
    {
        new Student { FirstName = "a", LastName = "a", MiddleName = "a", GroupId = 1 }, // Группа KT-44-21
        new Student { FirstName = "a", LastName = "a", MiddleName = "a", GroupId = 2 }, // Группа KT-44-21
        new Student { FirstName = "a", LastName = "a", MiddleName = "b", GroupId = 1 }, // Группа KT-41-21    

    };

            _context.Set<Student>().AddRange(students);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetStudentsByFioAsync_KT4421_TwoObjects() // тест по ФИО 
        {
            // Arrange
            var filter1 = new Filters.StudentFioFilters.StudentFioFilter
            {
                FirstName = "a",
                LastName = "a",
                MiddleName = "a"
            };

            // Act
            var studentsResult1 = await _studentService.GetStudentsByFioAsync(filter1, CancellationToken.None);

            // Assert
            Assert.Equal(2, studentsResult1.Length); // Ожидаем 1 студента
        }

        [Fact]
        public async Task GetStudentsByIdGroupAsync_KT4421_TwoObjects() // тест по GroupId 
        {
            // Act
            var filter2 = new Filters.GroupFilter.StudentIdGroup
            {
                GroupId = 1
            };

            var studentsResult2 = await _studentService.GetStudentsByIdGroupAsync(filter2, CancellationToken.None);

            // Assert
            Assert.Equal(2, studentsResult2.Length); // Ожидаем 1 студента
        }

        [Fact]
        public async Task GetStudentsByGroupNameAsync_KT4421_TwoObjects() // тест по GroupName
        {
            // Arrange
            var groupNameFilter = new Filters.StudentFilters.StudentGroupFilter
            {
                GroupName = "KT-44-21"
            };

            // Act
            var studentsResult = await _studentService.GetStudentsByGroupAsync(groupNameFilter, CancellationToken.None);

            // Assert
            Assert.Equal(2, studentsResult.Length); // Ожидаем 2 студента
        }

        public void Dispose()
        {
            // Удаление базы данных после тестов
            using (var context = new StudentDbContext(_dbContextOptions))
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
    

