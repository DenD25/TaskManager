using Infrastructure.DTOs.User;
using Infrastructure.Models;

namespace ApplicationCore.Contracts.Service
{
    public interface IJWTService
    {
        Task<string> GenerateTokenAsync(User user);
        Task<UserDto> CheckTokenAsync(string token);
    }
}
