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

    public async Task<Student> GetStudentNotTrackingByIdAsync(int id)
    {
        var student = await _studentRepository.GetStudentNotTrackingByIdAsync(id);

        return student;
    }

    public async Task<Student> GetStudentTrackingByIdAsync(int id)
    {
        var student = await _studentRepository.GetStudentTrackingByIdAsync(id);

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
        student.Update(student);

        await _studentRepository.UpdateStudentAsync(student);

        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteStudentAsync(int id)
    {
        await _studentRepository.DeleteStudentAsync(id);

        await _unitOfWork.CommitAsync();
    }
}