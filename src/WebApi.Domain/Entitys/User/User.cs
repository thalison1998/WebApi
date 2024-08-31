using WebApi.Domain.Entitys.Base;

namespace WebApi.Domain.Entitys.User
{
    public class User : EntityBase
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }

        private User(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public static User Create(string userName, string password)
        {
            return new User(userName, password);
        }
    }
}
