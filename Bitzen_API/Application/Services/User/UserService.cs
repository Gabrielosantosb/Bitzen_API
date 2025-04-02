using Bitzen_API.ORM.Entity;
using Bitzen_API.ORM.Model.User;

namespace Bitzen_API.Application.Services.User
{
    public class UserService : IUserService
    {
        public UserModel CreateUser(CreateUserModel createUserModel)
        {
            throw new NotImplementedException();
        }

        public bool IsEmailTaken(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateCredentials(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
