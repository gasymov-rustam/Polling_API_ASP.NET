using Microsoft.EntityFrameworkCore;
using Polling.Security.Dal;
using Polling.Security.Domain.Entities;

namespace Polling.Security.Services
{
    public class RefreshRepository : IRefreshRepository
    {
        private readonly SecurityContext _context;

        public RefreshRepository(SecurityContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RefreshToken token)
        {
            await _context.RefreshToken.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken> GetAsync(string token) => await _context.RefreshToken.FirstOrDefaultAsync(x => x.Token == token);

        public async Task<RefreshToken> UpdateAsync(RefreshToken token)
        {
            _context.RefreshToken.Update(token);
            await _context.SaveChangesAsync();

            return token;
        }
    }
}
