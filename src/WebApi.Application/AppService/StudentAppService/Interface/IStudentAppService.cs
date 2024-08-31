using WebApi.Application.Request;
using WebApi.Application.Response;

namespace WebApi.Application.AppService.StudentAppService.Interface;

public interface IStudentAppService
{
    Task<StudentResponse> GetStudentByIdAsync(int id);

    Task<StudentsListResponse> GetAllStudentsAsync();

    Task AddStudentAsync(AddStudentRequest request);

    Task UpdateStudentAsync(UpdateStudentRequest request);

    Task DeleteStudentAsync(int Id);
}