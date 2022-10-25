using Polling.Security.Domain.Entities;

namespace Polling.Security.Services
{
    public interface IRoleRepository
    {
        Task<Role?> GetRoleAsync(string name);
        Task<IReadOnlyList<Role>> GetAllRolesAsync();
        Task AddRoleAsync(Role role);
    }
}
