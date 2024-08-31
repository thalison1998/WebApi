using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApi.Infra.Data.Repositories.UserRepository;
using WebApi.Services.AuthService.Interface;

namespace WebApi.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly string _secretKey = "f633b084-46c1-4df2-ac8d-c53f687d642b";

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<string> Authenticate(string username, string password)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);

        if (user == null || !VerifyPassword(password, user.Password))
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new System.Security.Claims.ClaimsIdentity(new[]
            {
                    new System.Security.Claims.Claim("id", user.Id.ToString())
                }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private bool VerifyPassword(string password, string passwordUser)
    {
        return password == passwordUser;
    }
}

