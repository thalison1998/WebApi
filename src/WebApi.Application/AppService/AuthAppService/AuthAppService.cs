using WebApi.Application.AppService.AuthAppService.Interface;
using WebApi.Application.Request.Auth;
using WebApi.Application.Response.Auth;
using WebApi.Services.AuthService.Interface;

namespace WebApi.Application.AppService.AuthAppService;

public class AuthAppService : IAuthAppService
{
    private readonly IAuthService _authService;

    public AuthAppService(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var token = await _authService.Authenticate(request.Username, request.Password);

        if (token == null)
            return null;

        return new AuthResponse { Token = token };
    }
}

