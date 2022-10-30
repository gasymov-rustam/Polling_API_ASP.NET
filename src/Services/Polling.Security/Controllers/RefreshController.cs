using Microsoft.AspNetCore.Mvc;
using Polling.Security.Domain.Entities;
using Polling.Security.Services;
using System.Security.Claims;

namespace Polling.Security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefreshController : ControllerBase
    {
        private readonly IRefreshRepository _refreshRepository;
        private readonly ITokenStorage _tokenStorage;
        public RefreshController(IRefreshRepository refreshRepository, ITokenStorage tokenStorage)
        {
            _refreshRepository = refreshRepository;
            _tokenStorage = tokenStorage;
        }

        [HttpGet("/refresh")]
        public async Task<IActionResult> GetRefreshToken()
        {

            var token = _tokenStorage.Get();

            return Ok(await _refreshRepository.GetAsync(token.RefreshToken));
        }

        [HttpPut("/refresh")]
        public async Task<IActionResult> UpdateToken()
        {
            string rawUserId = HttpContext.User.FindFirstValue("UserId");
            var token = _tokenStorage.Get();

            return Ok(await _refreshRepository.UpdateAsync(new RefreshToken(token.RefreshToken)));
        }
    }
}

//public async Task<IActionResult> Logout()
//{
//    string rawUserId = HttpContext.User.FindFirstValue("UserId");

//    if (!int.TryParse(rawUserId, out int userId))
//    {
//        return Unauthorized();
//    }

//    await _refreshTokenRepository.DeleteAllAsync(userId);

//    return NoContent();
//}