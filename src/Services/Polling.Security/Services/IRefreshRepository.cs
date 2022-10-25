using Polling.Security.Domain.Entities;

namespace Polling.Security.Services
{
    public interface IRefreshRepository
    {
        Task<RefreshToken> GetAsync(string token);
        Task AddAsync(RefreshToken token);
        Task UpdateAsync(RefreshToken token);
    }
}
