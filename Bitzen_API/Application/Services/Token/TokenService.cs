using Bitzen_API.ORM.Entity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Bitzen_API.Application.Services.Token
{
    public class TokenService : ITokenService
    {


        private readonly IHttpContextAccessor _accessor;
        public TokenService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string GenerateToken(string key, string issuer, string audience, UserModel user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())            
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddDays(300),
            signingCredentials: credentials
        );

            var stringToken = new JwtSecurityTokenHandler().WriteToken(token);
            return stringToken;
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
            ;
        }

        #region JWT

        public string? GetUserEmail() => GetClaimsIdentity().FirstOrDefault(a => a.Type == ClaimTypes.Name)?.Value;
        public int GetUserId()
        {
            var claimValue = GetClaimsIdentity().FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
            if (claimValue == null) return 0;
            return int.Parse(claimValue);
        }

        #endregion JWT
        private IEnumerable<Claim> GetClaimsIdentity()
        {
            var test = _accessor.HttpContext.User;
            return _accessor.HttpContext.User.Claims;

        }
    }
}
