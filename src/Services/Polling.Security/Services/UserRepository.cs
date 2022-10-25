using Microsoft.EntityFrameworkCore;
using Polling.Security.Dal;
using Polling.Security.Domain.Entities;
using Shared.ServerResponse;

namespace Polling.Security.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly SecurityContext _context;

        public UserRepository(SecurityContext context)
        {
            _context = context;
        }

        public async Task CreateUserAsync(User user)
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<ServerResponse> DeleteUserByIdAsync(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return new ServerResponse("", true, user);
        }

        public async Task<ServerResponse> GetAllUsersAsync() => new ServerResponse("", true, await _context.User.ToListAsync());

        public async Task<User> GetOneUserByEmailAsync(string email) => await _context.User.FirstOrDefaultAsync(x => x.Email == email);

        public async Task<ServerResponse> UpdateUserAsync(User user)
        {
            var updatedUser = _context.User.Update(user);
            await _context.SaveChangesAsync();

            return new ServerResponse("", true, updatedUser);
        }
    }
}
