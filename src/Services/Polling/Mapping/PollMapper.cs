using Polling.Domain.Entities;
using Polling.Dto;

namespace Polling.Mapping
{
    public static class PollMapper
    {
        public static Poll CreatePollDtoToPoll(this CreatePollDto dto)
        {
            return new Poll
            {
                Author = dto.Author,
                Duration = dto.Duration,
                Name = dto.Name,
            };
        }

        public static Poll UpdatePollDtoToPoll(this UpdatePollDto dto, Poll existPoll)
        {
            existPoll.Duration = dto.Duration;
            existPoll.Name = dto.Name;
            existPoll.Author = dto.Author;

            return existPoll;
        }
    }
}
