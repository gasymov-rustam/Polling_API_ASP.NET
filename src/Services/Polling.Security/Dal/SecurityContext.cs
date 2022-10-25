using Microsoft.EntityFrameworkCore;
using Polling.Security.Domain.Entities;

namespace Polling.Security.Dal
{
    public class SecurityContext : DbContext
    {
        public virtual DbSet<User>? User { get; set; }
        public virtual DbSet<Role>? Role { get; set; }
        public virtual DbSet<RefreshToken>? RefreshToken { get; set; }

        public SecurityContext(DbContextOptions<SecurityContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Role>(options =>
            {
                options.HasMany(x => x.Users).WithOne(x => x.Role).HasForeignKey(x => x.RoleId);
            });

            base.OnModelCreating(builder);
        }
    }
}
