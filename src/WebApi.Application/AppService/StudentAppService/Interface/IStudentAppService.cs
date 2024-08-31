using WebApi.Application.Request.Student;
using WebApi.Application.Response.Student;

namespace WebApi.Application.AppService.StudentAppService.Interface;

public interface IStudentAppService
{
    Task<StudentResponse> GetStudentByIdAsync(int id);

    Task<StudentsListResponse> GetAllStudentsAsync();

    Task AddStudentAsync(AddStudentRequest request);

    Task UpdateStudentAsync(UpdateStudentRequest request);

    Task DeleteStudentAsync(int Id);
}