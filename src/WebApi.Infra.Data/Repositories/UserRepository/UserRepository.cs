using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Entitys.User;
using WebApi.Infra.Data.Context;

namespace WebApi.Infra.Data.Repositories.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly WebApiDbContext _context;

    public UserRepository(WebApiDbContext context)
    {
        _context = context;
    }

    public Task<User> GetUserByUsernameAsync(string username)
    {
        return _context.User.SingleOrDefaultAsync(u => u.UserName == username);
    }
}
