using Polling.Security.Domain.Entities;
using Shared.ServerResponse;

namespace Polling.Security.Services
{
    public interface IUserRepository
    {
        Task<ServerResponse> GetAllUsersAsync();
        Task<User> GetOneUserByEmailAsync(string email);
        Task CreateUserAsync(User user);
        Task<ServerResponse> UpdateUserAsync(User user);
        Task<ServerResponse> DeleteUserByIdAsync(int id);
    }
}
