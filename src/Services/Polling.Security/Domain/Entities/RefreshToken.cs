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

        public string Token { get; }
        public int UserId { get; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? RevokedAt { get; set; }
    }
}
