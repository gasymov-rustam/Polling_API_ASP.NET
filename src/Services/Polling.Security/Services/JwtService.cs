using Microsoft.IdentityModel.Tokens;
using Polling.Security.Domain;
using Polling.Security.Extensions;
using Polling.Security.TokenGenerator;
using Shared.Time;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Polling.Security.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private TokenValidationParameters _tokenValidationParameters;
        private readonly JwtOptions _options;
        private readonly IClock _clock;
        private readonly JwtTokenGenerator _tokenGenerator;

        public JwtService(JwtOptions options, IClock clock, JwtTokenGenerator tokenGenerator)
        {
            _options = options;
            _clock = clock;
            _tokenGenerator = tokenGenerator;
        }

        public JsonWebToken CreateToken(string userId, string role = null, IDictionary<string, string> claims = null)
        {
            var now = _clock.Current();

            var jwtClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimestamp().ToString()),
            };

            if (!string.IsNullOrWhiteSpace(role))
            {
                jwtClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var expires = now.AddMinutes(_options.Expiry);

            var jwt = _tokenGenerator.GenerateToken(_options.IssuerSigningKey,
                                                    _options.Issuer,
                                                    _options.Audience,
                                                    expires,
                                                    jwtClaims);

            var refreshJwt = _tokenGenerator.GenerateToken(_options.RefreshSigningKey,
                                                    _options.Issuer,
                                                    _options.Audience,
                                                    expires,
                                                    jwtClaims);

            return new JsonWebToken
            {
                AccessToken = jwt,
                RefreshToken = refreshJwt,
                Expires = expires.ToTimestamp(),
                UserId = userId,
                Role = role ?? "user",
                Claims = jwtClaims.ToDictionary(c => c.Type, c => c.Value)
            };
        }

        public JsonWebTokenPayload GetTokenPayload(string accessToken)
        {
            _tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.IssuerSigningKey)),
                ValidIssuer = _options.Issuer,
                ValidAudience = _options.Audience,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = _options.ValidateAudience,
                ClockSkew = TimeSpan.Zero
            };

            _jwtSecurityTokenHandler.ValidateToken(accessToken, _tokenValidationParameters,
               out var validatedSecurityToken);

            if (!(validatedSecurityToken is JwtSecurityToken jwt))
            {
                return null;
            }

            return new JsonWebTokenPayload
            {
                Subject = jwt.Subject,
                Role = jwt.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Role)?.Value,
                Expires = jwt.ValidTo.ToTimestamp(),
                Claims = jwt.Claims.ToDictionary(k => k.Type, v => v.Value)
            };
        }
    }
}
