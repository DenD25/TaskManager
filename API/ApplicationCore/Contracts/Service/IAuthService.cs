using Infrastructure.DTOs.Auth;
using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Contracts.Service
{
    public interface IAuthService
    {
        Task<IEnumerable<IdentityError>> RegisterAsync(RegisterDto userDto);
        Task<AuthUserDto> LoginAsync(LoginDto loginDto);
    }
}
