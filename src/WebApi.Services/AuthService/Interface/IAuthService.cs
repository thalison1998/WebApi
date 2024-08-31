namespace WebApi.Services.AuthService.Interface;

public interface IAuthService
{
    Task<string> Authenticate(string username, string password);
}