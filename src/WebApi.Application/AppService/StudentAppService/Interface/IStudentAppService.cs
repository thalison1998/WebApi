using WebApi.Application.Request.Student;
using WebApi.Application.Response.Custom;
using WebApi.Application.Response.Student;

namespace WebApi.Application.AppService.StudentAppService.Interface;

public interface IStudentAppService
{
    Task<StudentResponse> GetStudentByIdAsync(int id);

    Task<StudentsListResponse> GetAllStudentsAsync();

    Task<CustomResponse> AddStudentAsync(AddStudentRequest request);

    Task<CustomResponse> UpdateStudentAsync(UpdateStudentRequest request);

    Task<CustomResponse> DeleteStudentAsync(int id);
}