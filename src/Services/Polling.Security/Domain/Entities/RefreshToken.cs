using Shared.Abstractions;

namespace Polling.Security.Domain.Entities
{
    public sealed class RefreshToken : BaseEntity
    {
        public RefreshToken(string token,
                            int userId)
        {
            Token = token;
            UserId = userId;
        }
        public RefreshToken() { }

        public string Token { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? RevokedAt { get; set; }
        public User User { get; set; }
    }
}
