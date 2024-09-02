using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Entitys.Student;
using WebApi.Infra.Data.Repositories.StudentRepository;
using WebApi.Infra.Data.Context;
using Xunit;

public class StudentRepositoryTests
{
    private readonly StudentRepository _repository;
    private readonly WebApiDbContext _context;

    public StudentRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<WebApiDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new WebApiDbContext(options);
        _repository = new StudentRepository(_context);
    }

    [Fact]
    public async Task GetStudentNotTrackingByIdAsync_ShouldReturnStudent()
    {
        // Arrange
        var student = Student.Create("John Doe", 20, 10, 8.5, "123 Main St", "Father Name", "Mother Name", DateTime.Now);
        _context.Student.Add(student);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetStudentNotTrackingByIdAsync(student.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(student.Id, result.Id);
    }

    [Fact]
    public async Task GetAllStudentsAsync_ShouldReturnListOfStudents()
    {
        // Arrange
        var students = new List<Student>
        {
            Student.Create("John Doe", 20, 10, 8.5, "123 Main St", "Father Name", "Mother Name", DateTime.Now),
            Student.Create("Jane Doe", 22, 12, 9.0, "456 Elm St", "Father Name", "Mother Name", DateTime.Now)
        };
        _context.Student.AddRange(students);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetAllStudentsAsync();

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task AddStudentAsync_ShouldAddStudent()
    {
        // Arrange
        var student = Student.Create("John Doe", 20, 10, 8.5, "123 Main St", "Father Name", "Mother Name", DateTime.Now);

        // Act
        await _repository.AddStudentAsync(student);

        // Assert
        var result = await _context.Student.FindAsync(student.Id);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateStudentAsync_ShouldUpdateStudent()
    {
        // Arrange
        var student = Student.Create("John Doe", 20, 10, 8.5, "123 Main St", "Father Name", "Mother Name", DateTime.Now);
        _context.Student.Add(student);
        await _context.SaveChangesAsync();

        // Act
        student.Update(Student.Create("John Updated", 21, 11, 9.0, "789 Pine St", "New Father Name", "New Mother Name", DateTime.Now));
        await _repository.UpdateStudentAsync(student);

        // Assert
        var result = await _context.Student.FindAsync(student.Id);
        Assert.NotNull(result);
        Assert.Equal("John Updated", result.Name);
    }

    [Fact]
    public async Task DeleteStudentAsync_ShouldRemoveStudent()
    {
        // Arrange
        var student = Student.Create("John Doe", 20, 10, 8.5, "123 Main St", "Father Name", "Mother Name", DateTime.Now);
        _context.Student.Add(student);
        await _context.SaveChangesAsync();

        // Act
        await _repository.DeleteStudentAsync(student.Id);

        // Assert
        var result = await _context.Student.FindAsync(student.Id);
        Assert.Null(result);
    }
}
