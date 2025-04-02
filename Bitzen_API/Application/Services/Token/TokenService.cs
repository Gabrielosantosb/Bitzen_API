using Bitzen_API.ORM.Entity;

namespace Bitzen_API.Application.Services.Token
{
    public class TokenService : ITokenService
    {

        private readonly IHttpContextAccessor _acessor;
        public TokenService(IHttpContextAccessor acessor)
        {
            _acessor = acessor;
        }
        public string GenerateToken(string key, string issuer, string audience, UserModel user)
        {
            throw new NotImplementedException();
        }

        public string? GetUserEmail()
        {
            throw new NotImplementedException();
        }

        public int GetUserId()
        {
            throw new NotImplementedException();
        }
    }
}
