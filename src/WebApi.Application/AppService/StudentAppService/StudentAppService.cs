using WebApi.Application.AppService.StudentAppService.Interface;
using WebApi.Application.Request;
using WebApi.Application.Response;
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
            var student = await _studentService.GetStudentByIdAsync(id);

            if (student == null)
            {
                throw new KeyNotFoundException($"Student with ID {id} not found.");
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

        public async Task AddStudentAsync(AddStudentRequest request)
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
        }

        public async Task UpdateStudentAsync(UpdateStudentRequest request)
        {
            var student = Student.CreateForUpdate(
                request.Name,
                request.Age,
                request.Grade,
                request.AverageGrade,
                request.Address,
                request.FatherName,
                request.MotherName,
                request.BirthDate,
                request.Id
            );

            await _studentService.UpdateStudentAsync(student);
        }

        public async Task DeleteStudentAsync(int id)
        {
            await _studentService.DeleteStudentAsync(id);
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