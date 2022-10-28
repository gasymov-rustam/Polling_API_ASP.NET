using Microsoft.AspNetCore.Mvc;
using Polling.Security.Dto;
using Polling.Security.Services;

namespace Polling.Security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityRepository _securityRepository;
        private readonly ITokenStorage _tokenStorage;

        public SecurityController(ISecurityRepository securityRepository, ITokenStorage tokenStorage)
        {
            _securityRepository = securityRepository;
            _tokenStorage = tokenStorage;
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            await _securityRepository.Login(dto);
            var token = _tokenStorage.Get();

            return Ok(token);
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            await _securityRepository.Register(dto);
            var token = _tokenStorage.Get();

            return Ok(token);
        }
    }
}
