using Bitzen_API.ORM.Entity;
using Bitzen_API.ORM.Model.Common;
using Bitzen_API.ORM.Model.User;

namespace Bitzen_API.Application.Services.User
{
    public interface IUserService
    {
        Result<UserModel> CreateUser(CreateUserModel createUserModel);
        Result<UserModel> UpdateUser(int userId, UpdateUserModel updatedUserModel);
        Result<string> DeleteUser(int userId);

        Task<bool> ValidateCredentials(string email, string password);

        //UserModel UpdateUser(int userId, CreateUserModel createUserModel);
        //UserModel GetUser();

    }
}
