using Microsoft.AspNetCore.Mvc;
using Polling.Security.Domain.Entities;
using Polling.Security.Services;

namespace Polling.Security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("/user")]
        public async Task<IActionResult> GetAllUsersAsync() => Ok(await _userRepository.GetAllUsersAsync());

        [HttpGet("/user/{email}")]
        public async Task<IActionResult> GetOneUserByEmailAsync(string email) => Ok(await _userRepository.GetOneUserByEmailAsync(email));

        [HttpPost("/user")]
        public async Task<IActionResult> CreateUserAsync([FromBody] User user)
        {
            await _userRepository.CreateUserAsync(user);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("/user")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] User user) => Ok(await _userRepository.UpdateUserAsync(user));

        [HttpDelete("/user/{id}")]
        public async Task<IActionResult> DeleteUserByIdAsync(int id) => Ok(await _userRepository.DeleteUserByIdAsync(id));
    }
}
