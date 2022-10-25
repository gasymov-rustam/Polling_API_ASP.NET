using Polling.Security.Domain;

namespace Polling.Security.Services
{
    public class TokenStorage : ITokenStorage
    {
        private const string TokenKey = "jwt";
        private readonly IHttpContextAccessor _contextAccessor;

        public TokenStorage(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public JsonWebToken? Get()
        {
            if (_contextAccessor.HttpContext is null) return null;

            if (_contextAccessor.HttpContext.Items.TryGetValue(TokenKey, out var jwt)) return jwt as JsonWebToken;

            return null;
        }

        public void Set(JsonWebToken jwt) => _contextAccessor.HttpContext?.Items.TryAdd(TokenKey, jwt);
    }
}
