using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using WebApi.Application.AppService.StudentAppService.Interface;
using WebApi.Application.Request.Student;
using WebApi.Application.Response.Student;

namespace WebApi.Api.Controllers.StudentController
{
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentAppService _studentAppService;

        public StudentController(IStudentAppService studentAppService)
        {
            _studentAppService = studentAppService;
        }

        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Student retrieved successfully.", typeof(StudentResponse))]
        [SwaggerOperation(Summary = "Get a student by ID")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            try
            {
                var student = await _studentAppService.GetStudentByIdAsync(id);
                return Ok(student);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "List of students retrieved successfully.", typeof(StudentsListResponse))]
        [SwaggerOperation(Summary = "Get all students")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentAppService.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Student created successfully.")]
        [SwaggerOperation(Summary = "Add a new student")]
        public async Task<IActionResult> AddStudent([FromBody] AddStudentRequest request)
        {
            await _studentAppService.AddStudentAsync(request);
            return Ok();
        }

        [HttpPut]
        [SwaggerResponse((int)HttpStatusCode.OK, "Student updated successfully.")]
        [SwaggerOperation(Summary = "Update an existing student")]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentRequest request)
        {
            await _studentAppService.UpdateStudentAsync(request);
            return Ok();
        }

        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NoContent, "Student deleted successfully.")]
        [SwaggerOperation(Summary = "Delete a student by ID")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _studentAppService.DeleteStudentAsync(id);
            return NoContent();
        }
    }
}
