﻿namespace Polling.Security.Domain
{
    public sealed class JsonWebToken
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long Expires { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public IDictionary<string, string> Claims { get; set; }
    }
}
