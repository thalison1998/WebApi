using WebApi.Domain.Entitys.Student;
using WebApi.Infra.Data.Repositories.StudentRepository;
using WebApi.Infra.Data.UnitOfWork;
using WebApi.Services.StudentService.Interface;

namespace WebApi.Services.StudentService;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public StudentService(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
    {
        _studentRepository = studentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Student> GetStudentByIdAsync(int id)
    {
        var student = await _studentRepository.GetStudentByIdAsync(id);

        return student;
    }

    public async Task<IEnumerable<Student>> GetAllStudentsAsync()
    {
        return await _studentRepository.GetAllStudentsAsync();
    }

    public async Task AddStudentAsync(Student student)
    {
        await _studentRepository.AddStudentAsync(student);

        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateStudentAsync(Student student)
    {
        await _studentRepository.UpdateStudentAsync(student);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteStudentAsync(int id)
    {
        var student = await _studentRepository.GetStudentByIdAsync(id);
   
        await _studentRepository.DeleteStudentAsync(student.Id);

        await _unitOfWork.CommitAsync();
    }
}
