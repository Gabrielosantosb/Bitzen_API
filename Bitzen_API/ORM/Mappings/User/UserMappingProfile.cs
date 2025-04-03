using AutoMapper;
using Bitzen_API.ORM.Entity;
using Bitzen_API.ORM.Model.User;
namespace Bitzen_API.ORM.Mappings.User
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile() {

            CreateMap<CreateUserModel, UserModel>();                 
        }

    }
}

