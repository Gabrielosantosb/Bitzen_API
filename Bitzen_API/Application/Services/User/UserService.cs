using AutoMapper;
using Bitzen_API.Application.Services.Token;
using Bitzen_API.ORM.Entity;
using Bitzen_API.ORM.Model.Common;
using Bitzen_API.ORM.Model.User;
using Bitzen_API.ORM.Repository;

namespace Bitzen_API.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly BaseRepository<UserModel> _userRepository;
        private ITokenService _tokenService { get; }
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(BaseRepository<UserModel> userRepository, ITokenService tokenService, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<Result<LoginResponseModel>> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.FindAsync(e => e.Email == email);
            if (user == null || !VerifyPassword(password, user.Password))
                return Result<LoginResponseModel>.Fail("E-mail ou senha inválidos.");

            var token = _tokenService.GenerateToken(
                _configuration["Jwt:Key"],
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                user
            );

            var response = new LoginResponseModel
            {
                Token = token,
                Username = user.Name,
                Message = "Login realizado com sucesso."
            };

            return Result<LoginResponseModel>.Ok(response);
        }



        public Result<UserModel> CreateUser(CreateUserModel createUserModel)
        {
            if (IsEmailTaken(createUserModel.Email))
                return Result<UserModel>.Fail("E-mail já está em uso.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(createUserModel.Password);

            var newUser = _mapper.Map<UserModel>(createUserModel);
            newUser.Password = hashedPassword;

            var res = _userRepository.Add(newUser);
            _userRepository.SaveChanges();

            return Result<UserModel>.Ok(res);
        }

        public Result<UserModel> UpdateUser(int userId, UpdateUserModel updatedUserModel)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                return Result<UserModel>.Fail("Usuário não encontrado.");

            if (user.Email != updatedUserModel.Email && IsEmailTaken(updatedUserModel.Email))
                return Result<UserModel>.Fail("E-mail já está em uso por outro usuário.");
            
            _mapper.Map(updatedUserModel, user);

            if (!string.IsNullOrWhiteSpace(updatedUserModel.Password))
                user.Password = BCrypt.Net.BCrypt.HashPassword(updatedUserModel.Password);

            _userRepository.Update(user);
            _userRepository.SaveChanges();

            return Result<UserModel>.Ok(user);
        }

        public Result<string> DeleteUser(int userId)
        {
            var user = _userRepository.GetById(userId);
            if(user == null) return Result<string>.Fail("Usuário não encontrado.");
            _userRepository.Delete(user);
            _userRepository.SaveChanges();
            return Result<string>.Ok("Usuário deletado com sucesso.");
        }
        private bool IsEmailTaken(string email)
        {
            return _userRepository.FindAll(e => e.Email == email).Any();

        }

        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedPassword);
        }     
    }
}
