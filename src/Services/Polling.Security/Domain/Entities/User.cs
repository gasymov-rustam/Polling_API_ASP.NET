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
        public User()
        {
            Tokens = new HashSet<RefreshToken>();
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public DateTimeOffset CreatedAt { get; } = DateTimeOffset.UtcNow;

        public Role Role { get; set; }
        public ICollection<RefreshToken> Tokens { get; set; }
    }
}
