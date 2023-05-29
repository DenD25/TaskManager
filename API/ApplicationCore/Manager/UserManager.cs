using ApplicationCore.Contracts.Service;
using AutoMapper;
using Infrastructure.DTOs.User;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using TaskManagerAPI.Contracts.Manager;
using TaskManagerAPI.Contracts.Repository;
using TaskManagerAPI.Contracts.Service;
using TaskManagerAPI.DTOs.User;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Manager
{
    public class UserManager: IUserManager
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IPhotoService _photoService;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        public UserManager( IMapper mapper, IUserRepository userRepository, UserManager<User> userManager, IPhotoService photoService, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
            _userManager = userManager;
            _photoService = photoService;
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

        public async Task<PhotoDto> AddPhotoAsync(IFormFile file)
        {
            var user = await _userRepository.UserByIdAsync(int.Parse(_userService.GetUserId()));

            if (user.Photo != null)
            {
                var deletionResult = await _photoService.DeletePhotoAsync(user.Photo.PublicId);

                if (deletionResult.Error != null)
                    throw new Exception(deletionResult.Error.ToString());
            }

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null)
                throw new Exception(result.Error.ToString());

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                UserId = user.Id,
            };

            user.Photo = photo;

            await _userRepository.UpdateAsync(user);

            var photoDto = _mapper.Map<PhotoDto>(photo);

            return photoDto;
        }

        private async Task<UserDto> MapUserToUserDto(User user)
        {
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Roles = await _userManager.GetRolesAsync(user);

            return userDto;
        }
    }
}
