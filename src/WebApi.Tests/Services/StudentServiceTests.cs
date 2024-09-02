using Moq;
using WebApi.Domain.Entitys.Student;
using WebApi.Infra.Data.Repositories.StudentRepository;
using WebApi.Infra.Data.UnitOfWork;
using WebApi.Services.StudentService;
using Xunit;

namespace WebApi.Tests.Services
{
    public class StudentServiceTests
    {
        private readonly Mock<IStudentRepository> _mockStudentRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly StudentService _studentService;

        public StudentServiceTests()
        {
            _mockStudentRepository = new Mock<IStudentRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _studentService = new StudentService(_mockStudentRepository.Object, _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetStudentNotTrackingByIdAsync_ShouldReturnStudent()
        {
            // Arrange
            var studentId = 1;
            var expectedStudent = new Student { Id = studentId };
            _mockStudentRepository
                .Setup(repo => repo.GetStudentNotTrackingByIdAsync(studentId))
                .ReturnsAsync(expectedStudent);

            // Act
            var result = await _studentService.GetStudentNotTrackingByIdAsync(studentId);

            // Assert
            Assert.Equal(expectedStudent, result);
        }

        [Fact]
        public async Task GetStudentTrackingByIdAsync_ShouldReturnStudent()
        {
            // Arrange
            var studentId = 1;
            var expectedStudent = new Student { Id = studentId };
            _mockStudentRepository
                .Setup(repo => repo.GetStudentTrackingByIdAsync(studentId))
                .ReturnsAsync(expectedStudent);

            // Act
            var result = await _studentService.GetStudentTrackingByIdAsync(studentId);

            // Assert
            Assert.Equal(expectedStudent, result);
        }

        [Fact]
        public async Task GetAllStudentsAsync_ShouldReturnAllStudents()
        {
            // Arrange
            var students = new List<Student> { new Student { Id = 1 }, new Student { Id = 2 } };
            _mockStudentRepository
                .Setup(repo => repo.GetAllStudentsAsync())
                .ReturnsAsync(students);

            // Act
            var result = await _studentService.GetAllStudentsAsync();

            // Assert
            Assert.Equal(students, result);
        }

        [Fact]
        public async Task AddStudentAsync_ShouldAddStudentAndCommit()
        {
            // Arrange
            var student = new Student { Id = 1 };
            _mockStudentRepository
                .Setup(repo => repo.AddStudentAsync(student))
                .Returns(Task.CompletedTask);

            // Act
            await _studentService.AddStudentAsync(student);

            // Assert
            _mockStudentRepository.Verify(repo => repo.AddStudentAsync(student), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateStudentAsync_ShouldUpdateStudentAndCommit()
        {
            // Arrange
            var student = Student.CreateForUpdate("Teste",2,4,5.5,"Teste", "Teste", "Teste",DateTime.UtcNow,1);

            _mockStudentRepository
                .Setup(repo => repo.UpdateStudentAsync(student))
                .Returns(Task.CompletedTask);

            // Act
            await _studentService.UpdateStudentAsync(student);

            // Assert
            _mockStudentRepository.Verify(repo => repo.UpdateStudentAsync(student), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteStudentAsync_ShouldDeleteStudentAndCommit()
        {
            // Arrange
            var studentId = 1;
            _mockStudentRepository
                .Setup(repo => repo.DeleteStudentAsync(studentId))
                .Returns(Task.CompletedTask);

            // Act
            await _studentService.DeleteStudentAsync(studentId);

            // Assert
            _mockStudentRepository.Verify(repo => repo.DeleteStudentAsync(studentId), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);
        }
    }
}

