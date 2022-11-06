using Microsoft.AspNetCore.Mvc;
using Polling.Dto;
using Polling.Repository.Interfaces;

namespace Polling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollController : ControllerBase
    {
        private readonly IPollService _service;

        public PollController(IPollService service)
        {
            _service = service;
        }

        [HttpGet("/polls")]
        public async Task<ActionResult> GetPolls()
        {
            return Ok(await _service.GetAllPolls());
        }
        
        [HttpGet("/polls/{id}")]
        public async Task<ActionResult> GetPollById(int id)
        {
            return Ok(await _service.GetPollById(id));
        }
        
        [HttpPost("/polls")]
        public async Task<ActionResult> CreatePoll([FromBody] CreatePollDto dto)
        {
            return Ok(await _service.CreatePollAsync(dto));
        }
        
        [HttpPut("/polls")]
        public async Task<ActionResult> UpdatePoll([FromBody] UpdatePollDto dto)
        {
            return Ok(await _service.UpdatePollAsync(dto));
        }
        
        [HttpDelete("/polls/{id}")]
        public async Task<ActionResult> DeletePoll(int id)
        {
            return Ok(await _service.DeletePollById(id));
        }
    }
}
