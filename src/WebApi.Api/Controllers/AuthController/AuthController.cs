using Microsoft.AspNetCore.Mvc;
using WebApi.Api.Controllers.BaseController;
using WebApi.Application.AppService.AuthAppService.Interface;
using WebApi.Application.Request.Auth;
using System.Net;

namespace WebApi.Api.Controllers.AuthController
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class AuthController : Base
    {
        private readonly IAuthAppService _authAppService;

        public AuthController(IAuthAppService authAppService)
        {
            _authAppService = authAppService;
        }

        [HttpPost("login")]
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
