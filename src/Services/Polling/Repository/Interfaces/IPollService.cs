using Polling.Dto;
using Shared.ServerResponse;

namespace Polling.Repository.Interfaces
{
    public interface IPollService
    {
        Task<ServerResponse> GetAllPolls();
        Task<ServerResponse> GetPollById(int id);
        Task<ServerResponse> CreatePollAsync(CreatePollDto dto);
        Task<ServerResponse> UpdatePollAsync(UpdatePollDto dto);
        Task<ServerResponse> DeletePollById(int id);
    }
}
