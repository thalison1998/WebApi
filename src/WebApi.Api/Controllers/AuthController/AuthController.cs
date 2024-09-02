using Microsoft.AspNetCore.Mvc;
using WebApi.Api.Controllers.BaseController;
using WebApi.Application.AppService.AuthAppService.Interface;
using WebApi.Application.Request.Auth;
using System.Net;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Api.Controllers.AuthController
{
    public class AuthController : Base
    {
        private readonly IAuthAppService _authAppService;

        public AuthController(IAuthAppService authAppService)
        {
            _authAppService = authAppService;
        }

        [HttpPost("login")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "An unexpected error occurred.", typeof(ProblemDetails))]
        [SwaggerOperation(Summary = "Perform authentication")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _authAppService.LoginAsync(request);

            if (response == null)
            {
                return CustomControllerError("Username or password is incorrect", HttpStatusCode.Unauthorized);
            }

            return CustomControllerResponse(response);
        }
    }
}
