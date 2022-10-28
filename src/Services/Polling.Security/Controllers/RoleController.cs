using Microsoft.AspNetCore.Mvc;
using Polling.Security.Domain.Entities;
using Polling.Security.Services;

namespace Polling.Security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet("/role")]
        public async Task<IActionResult> GetRoleAsync([FromBody] string role) => Ok(await _roleRepository.GetRoleAsync(role));

        [HttpGet("/roles")]
        public async Task<IActionResult> GetAllRolesAsync() => Ok(await _roleRepository.GetAllRolesAsync());

        [HttpPost("/role")]
        public async Task<IActionResult> AddRoleAsync([FromBody] string name)
        {
            var role = new Role()
            {
                Name = name
            };
            await _roleRepository.AddRoleAsync(role);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
