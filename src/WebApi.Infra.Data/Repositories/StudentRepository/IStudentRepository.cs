using WebApi.Domain.Entitys.Student;

namespace WebApi.Infra.Data.Repositories.StudentRepository;

public interface IStudentRepository : IDisposable
{
    Task<IEnumerable<Student>> GetAllStudentsAsync();
    Task<Student> GetStudentByIdAsync(int id);
    Task AddStudentAsync(Student student);
    Task UpdateStudentAsync(Student student);
    Task DeleteStudentAsync(int id);
}