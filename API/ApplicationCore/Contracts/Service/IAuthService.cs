using Microsoft.AspNetCore.Identity;
using TaskManagerAPI.DTOs.Auth;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Contracts.Service
{
    public interface IAuthService
    {
        Task<IEnumerable<IdentityError>> RegisterAsync(RegisterDto userDto);
        Task<AuthUserDto> LoginAsync(LoginDto loginDto);
    }
}
