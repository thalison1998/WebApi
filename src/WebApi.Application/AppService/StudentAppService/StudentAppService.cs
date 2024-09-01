using WebApi.Application.AppService.StudentAppService.Interface;
using WebApi.Application.Request.Student;
using WebApi.Application.Response.Custom;
using WebApi.Application.Response.Student;
using WebApi.Domain.Entitys.Student;
using WebApi.Services.StudentService.Interface;

namespace WebApi.Application.AppService.StudentAppService
{
    public class StudentAppService : IStudentAppService
    {
        private readonly IStudentService _studentService;

        public StudentAppService(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<StudentResponse> GetStudentByIdAsync(int id)
        {
            var student = await _studentService.GetStudentNotTrackingByIdAsync(id);

            if (student == null)
            {
                return null;
            }
            return MapToResponse(student);
        }

        public async Task<StudentsListResponse> GetAllStudentsAsync()
        {
            var students = await _studentService.GetAllStudentsAsync();

            return new StudentsListResponse
            {
                Students = students.Select(MapToResponse).ToList()
            };
        }

        public async Task<CustomResponse> AddStudentAsync(AddStudentRequest request)
        {
            var student = Student.Create(
                request.Name,
                request.Age,
                request.Grade,
                request.AverageGrade,
                request.Address,
                request.FatherName,
                request.MotherName,
                request.BirthDate
            );

            await _studentService.AddStudentAsync(student);

            return new CustomResponse { Message = "student record created successfully", Id = student.Id };
        }

        public async Task<CustomResponse> UpdateStudentAsync(UpdateStudentRequest request)
        {
            var studentBroughtByGetById = await _studentService.GetStudentTrackingByIdAsync(request.Id);

            if (studentBroughtByGetById == null)
            {
                return null;
            }

            await _studentService.UpdateStudentAsync(studentBroughtByGetById);

            return new CustomResponse { Message = "Student record updated successfully", Id = studentBroughtByGetById.Id };
        }

        public async Task<CustomResponse> DeleteStudentAsync(int id)
        {
            var studentBroughtByGetById = await _studentService.GetStudentTrackingByIdAsync(id);

            if (studentBroughtByGetById == null)
            {
                return null;
            }

            await _studentService.DeleteStudentAsync(studentBroughtByGetById.Id);

            return new CustomResponse { Message = "student record successfully deleted", Id = studentBroughtByGetById.Id };
        }

        private StudentResponse MapToResponse(Student student)
        {
            return new StudentResponse
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                Grade = student.Grade,
                AverageGrade = student.AverageGrade,
                Address = student.Address,
                FatherName = student.FatherName,
                MotherName = student.MotherName,
                BirthDate = student.BirthDate
            };
        }
    }
}