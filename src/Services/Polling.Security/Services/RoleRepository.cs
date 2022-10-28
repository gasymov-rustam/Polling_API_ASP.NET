using Microsoft.EntityFrameworkCore;
using Polling.Security.Dal;
using Polling.Security.Domain.Entities;

namespace Polling.Security.Services
{
    public class RoleRepository : IRoleRepository
    {
        private readonly SecurityContext _context;

        public RoleRepository(SecurityContext context)
        {
            _context = context;
        }

        public async Task AddRoleAsync(Role role)
        {
            await _context.Role.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Role>> GetAllRolesAsync() => await _context.Role.ToListAsync();

        public async Task<Role?> GetRoleAsync(string name) => await _context.Role.SingleOrDefaultAsync(x => x.Name == name);

        public async Task<Role?> GetRoleByIDAsync(int id) => await _context.Role.SingleOrDefaultAsync(x => x.Id == id);
    }
}
