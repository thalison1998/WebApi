using WebApi.Domain.Entitys.Student;

namespace WebApi.Infra.Data.Repositories.StudentRepository;

public interface IStudentRepository : IDisposable
{
    Task<IEnumerable<Student>> GetAllStudentsAsync();
    Task<Student> GetStudentNotTrackingByIdAsync(int id);
    Task AddStudentAsync(Student student);
    Task UpdateStudentAsync(Student student);
    Task DeleteStudentAsync(int id);
    Task<Student> GetStudentTrackingByIdAsync(int id);
}