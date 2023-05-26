using Infrastructure.DTOs.User;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Contracts.Service
{
    public interface IJWTService
    {
        Task<string> GenerateTokenAsync(User user);
        Task<UserDto> CheckTokenAsync(string token);
    }
}
