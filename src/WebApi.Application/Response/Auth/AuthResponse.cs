namespace WebApi.Application.Response.Auth;

public class AuthResponse
{
    public string Token { get; set; }
    public string Message { get; set; }
    public bool Sucess { get; set; } = false;
}
