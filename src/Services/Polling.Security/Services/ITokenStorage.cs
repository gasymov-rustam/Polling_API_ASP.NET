using Polling.Security.Domain;

namespace Polling.Security.Services
{
    public interface ITokenStorage
    {
        void Set(JsonWebToken jwt);
        JsonWebToken? Get();
    }
}
