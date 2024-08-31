using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AppService.AuthAppService.Interface;
using WebApi.Application.Request.Auth;

namespace WebApi.Api.Controllers.AuthController
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
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
                return Unauthorized(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
    }
}
