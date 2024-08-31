using WebApi.Application.Request.Auth;
using WebApi.Application.Response.Auth;

namespace WebApi.Application.AppService.AuthAppService.Interface;

public interface IAuthAppService
{
    Task<AuthResponse> LoginAsync(LoginRequest request);
}

