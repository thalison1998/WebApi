using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using WebApi.Api.Controllers.BaseController;
using WebApi.Application.AppService.StudentAppService.Interface;
using WebApi.Application.Request.Student;
using WebApi.Application.Response.Custom;
using WebApi.Application.Response.Student;

namespace WebApi.Api.Controllers.StudentController
{
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
   
    public class StudentController : Base
    {
        private readonly IStudentAppService _studentAppService;

        public StudentController(IStudentAppService studentAppService)
        {
            _studentAppService = studentAppService;
        }

        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Student retrieved successfully.", typeof(StudentResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Student not found.", typeof(ProblemDetails))]
        [SwaggerOperation(Summary = "Get a student by ID")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            try
            {
                var student = await _studentAppService.GetStudentByIdAsync(id);
                return student != null
                    ? CustomControllerResponse(student)
                    : CustomControllerError("Student not found", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "List of students retrieved successfully.", typeof(StudentsListResponse))]
        [SwaggerOperation(Summary = "Get all students")]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var students = await _studentAppService.GetAllStudentsAsync();
                return CustomControllerResponse(students);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Student created successfully.", typeof(CustomResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid request data.", typeof(ProblemDetails))]
        [SwaggerOperation(Summary = "Add a new student")]
        public async Task<IActionResult> AddStudent([FromBody] AddStudentRequest request)
        {
            try
            {
                CustomResponse response = await _studentAppService.AddStudentAsync(request);
                return CustomControllerResponse(response, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPut]
        [SwaggerResponse((int)HttpStatusCode.OK, "Student updated successfully.", typeof(CustomResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid request data.", typeof(ProblemDetails))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Student not found.", typeof(ProblemDetails))]
        [SwaggerOperation(Summary = "Update an existing student")]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentRequest request)
        {
            try
            {
                CustomResponse response = await _studentAppService.UpdateStudentAsync(request);
                return CustomControllerResponse(response);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NoContent, "Student deleted successfully.", typeof(CustomResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Student not found.", typeof(ProblemDetails))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "An unexpected error occurred.", typeof(ProblemDetails))]
        [SwaggerOperation(Summary = "Delete a student by ID")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                CustomResponse response = await _studentAppService.DeleteStudentAsync(id);
                return CustomControllerResponse(response);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}