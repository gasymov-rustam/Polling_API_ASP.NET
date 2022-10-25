using Shared.Abstractions;

namespace Polling.Security.Domain.Entities
{
    public class User : BaseEntity
    {
        public User(string email,
                    string password,
                    int roleId)
        {
            Email = email;
            Password = password;
            RoleId = roleId;
        }

        public string Email { get; }
        public string Password { get; }
        public int RoleId { get; }
        public DateTimeOffset CreatedAt { get; } = DateTimeOffset.UtcNow;

        public Role Role { get; }
    }
}
