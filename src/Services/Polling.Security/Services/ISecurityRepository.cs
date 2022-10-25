using Polling.Security.Dto;

namespace Polling.Security.Services
{
    public interface ISecurityRepository
    {
        Task Login(LoginDto dto);
        Task Register(RegisterDto dto);
    }
}
