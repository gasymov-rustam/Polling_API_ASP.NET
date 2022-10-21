namespace Polling.Security.Services
{
    public interface IAccessTokenService
    {
        Task<bool> IsCurrentActiveToken();
        Task DeactivateCurrentAsync(string userId);
        Task<bool> IsActiveAsync(string token);
        Task DeactivateAsync(string userId, string token);
    }
}
