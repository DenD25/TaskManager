using Infrastructure.DTOs.User;
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
    }
}
