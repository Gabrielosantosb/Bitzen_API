using Bitzen_API.ORM.Entity;

namespace Bitzen_API.Application.Services.Token
{
    public interface ITokenService
    {
        string GenerateToken(string key, string issuer, string audience, UserModel user);
        string? GetUserEmail();
        int GetUserId();
    }
}
