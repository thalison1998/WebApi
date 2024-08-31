using WebApi.Domain.Entitys.Base;

namespace WebApi.Domain.Entitys.User;

public class User : EntityBase
{
    public string UserName { get; private set; }
    public string Password { get; private set; }
}

