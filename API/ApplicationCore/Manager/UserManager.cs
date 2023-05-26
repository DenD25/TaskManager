using AutoMapper;
using Infrastructure.DTOs.User;
using Microsoft.AspNetCore.Identity;
using TaskManagerAPI.Contracts.Manager;
using TaskManagerAPI.Contracts.Repository;
using TaskManagerAPI.DTOs.User;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Manager
{
    public class UserManager: IUserManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        public UserManager( IMapper mapper, IUserRepository userRepository, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<UserDto> GetUserByUserNameAsync(string username)
        {
            var user = await _userRepository.UserByUsernameAsync(username);

            var usersDto = await MapUserToUserDto(user);

            return usersDto;
        }

        public async Task<UserDto> GetUserByUserIdAsync(int id)
        {
            var user = await _userRepository.UserByIdAsync(id);

            var usersDto = await MapUserToUserDto(user);

            return usersDto;
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            var users = await _userRepository.UsersAsync();

            var usersDto = _mapper.Map<List<UserDto>>(users);

            foreach (var user in usersDto)
            {
                var userModel = await _userRepository.UserByUsernameAsync(user.UserName);
                user.Roles = await _userManager.GetRolesAsync(userModel);
            }

            return usersDto;
        }

        public async Task<UserDto> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.UserByIdAsync(updateUserDto.Id);
            _mapper.Map(updateUserDto, user);

            await _userRepository.UpdateAsync(user);

            var userDto = await GetUserByUserIdAsync(updateUserDto.Id);

            return userDto;
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        private async Task<UserDto> MapUserToUserDto(User user)
        {
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Roles = await _userManager.GetRolesAsync(user);

            return userDto;
        }
    }
}
