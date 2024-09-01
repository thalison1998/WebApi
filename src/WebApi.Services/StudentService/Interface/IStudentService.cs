using WebApi.Domain.Entitys.Student;

namespace WebApi.Services.StudentService.Interface;

public interface IStudentService
{
    Task<Student> GetStudentTrackingByIdAsync(int id);
    Task<Student> GetStudentNotTrackingByIdAsync(int id);
    Task<IEnumerable<Student>> GetAllStudentsAsync();
    Task AddStudentAsync(Student student);
    Task UpdateStudentAsync(Student student);
    Task DeleteStudentAsync(int id);
}