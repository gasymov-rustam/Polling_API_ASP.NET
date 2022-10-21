namespace Polling.Security.Domain
{
    public sealed class JwtOptions
    {
        public string? Issuer { get; set; }
        public string? IssuerSigningKey { get; set; }
        public string? Audience { get; set; }
        public int Expiry { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateLifetime { get; set; }
    }
}
