using Polling.Security.Domain;

namespace Polling.Security.Services
{
    public interface IJwtService
    {
        JsonWebToken CreateToken(string userId, string role = null, IDictionary<string, string> claims = null);
        JsonWebTokenPayload GetTokenPayload(string accessToken);
    }
}
