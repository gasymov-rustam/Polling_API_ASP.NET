using Microsoft.EntityFrameworkCore;
using Polling.Dal;
using Polling.Dto;
using Polling.Mapping;
using Polling.Repository.Interfaces;
using Shared.ServerResponse;

namespace Polling.Repository.Implementations
{
    public class PollService : IPollService
    {
        private readonly PollingContext _context;

        public PollService(PollingContext context)
        {
            _context = context;
        }

        public async Task<ServerResponse> GetAllPolls()
        {
            var polls = await _context.Poll.ToListAsync();

            return new ServerResponse("", true, polls);
        }

        public async Task<ServerResponse> GetPollById(int id)
        {
            var poll = await _context.Poll.FirstOrDefaultAsync(x => x.Id == id);

            if (poll is null)
                throw new Exception("");

            return new ServerResponse("", true, poll);
        }

        public async Task<ServerResponse> CreatePollAsync(CreatePollDto dto)
        {
            var poll = await _context.Poll.FirstOrDefaultAsync(x => x.Name == dto.Name);

            if(poll is not null)
                throw new Exception("");

            var mapper = PollMapper.CreatePollDtoToPoll(dto);
            
            await _context.Poll.AddAsync(mapper);
            await _context.SaveChangesAsync();

            return new ServerResponse("", true, mapper);
        }


        public async Task<ServerResponse> UpdatePollAsync(UpdatePollDto dto)
        {
            var poll = await _context.Poll.FirstOrDefaultAsync(x => x.Name == dto.Name);

            if (poll is null)
                throw new Exception("");

            var mapper = PollMapper.UpdatePollDtoToPoll(dto, poll);

            _context.Poll.Update(mapper);
            await _context.SaveChangesAsync();

            return new ServerResponse("", true, mapper);
        }
        
        public async Task<ServerResponse> DeletePollById(int id)
        {
            var poll = await _context.Poll.FirstOrDefaultAsync(x => x.Id == id);

            if (poll is null)
                throw new Exception("");

            _context.Poll.Remove(poll);
            await _context.SaveChangesAsync();

            return new ServerResponse("", true, poll);
        }
    }
}
