using Microsoft.AspNetCore.Mvc;
using Polling.Security.Domain;
using Polling.Security.Domain.Entities;
using Polling.Security.Services;
using Polling.Security.TokenGenerator;
using Shared.Time;

namespace Polling.Security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefreshController : ControllerBase
    {
        private readonly IRefreshRepository _refreshRepository;
        private readonly JwtTokenGenerator _tokenGenerator;
        private readonly JwtOptions _options;
        private readonly IClock _clock;

        public RefreshController(IRefreshRepository refreshRepository, JwtTokenGenerator tokenGenerator, JwtOptions options, IClock clock)
        {
            _refreshRepository = refreshRepository;
            _tokenGenerator = tokenGenerator;
            _options = options;
            _clock = clock;
        }

        [HttpGet("/get/refresh")]
        public async Task<IActionResult> GetRefreshToken([FromBody] string token)
        {
            return Ok(await _refreshRepository.GetAsync(token));
        }

        [HttpPut("/refresh")]
        public async Task<IActionResult> UpdateToken([FromBody] string token)
        {
            var now = _clock.Current();

            var expires = now.AddMinutes(_options.Expiry);

            var existingToken = await _refreshRepository.GetAsync(token);

            if (existingToken is null)
                throw new Exception("refresh_token_is_null");

            var refreshJwt = _tokenGenerator.GenerateToken(_options.RefreshSigningKey,
                                                   _options.Issuer,
                                                   _options.Audience,
                                                   expires);

            return Ok(await _refreshRepository.UpdateAsync(new RefreshToken 
            { 
                Token = refreshJwt,
                RevokedAt = DateTime.UtcNow
            }));
        }
    }
}