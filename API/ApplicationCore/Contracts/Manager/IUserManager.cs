using Infrastructure.DTOs.User;
using Microsoft.AspNetCore.Http;
using TaskManagerAPI.DTOs.User;

namespace TaskManagerAPI.Contracts.Manager
{
    public interface IUserManager
    {
        Task<UserDto> GetUserByUserNameAsync(string username);
        Task<UserDto> GetUserByUserIdAsync(int id);
        Task<List<UserDto>> GetUsersAsync();
        Task<UserDto> UpdateUserAsync(UpdateUserDto userDto);
        Task DeleteUserAsync(int id);
        Task<PhotoDto> AddPhotoAsync(IFormFile file);
    }
}
