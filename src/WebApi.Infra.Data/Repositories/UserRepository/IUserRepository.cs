using WebApi.Domain.Entitys.User;

namespace WebApi.Infra.Data.Repositories.UserRepository;

public interface IUserRepository
{
    Task<User> GetUserByUsernameAsync(string username);
}

