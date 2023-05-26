using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Contracts.Service;
using TaskManagerAPI.DTOs.Auth;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IJWTService _jWTService;

        public AuthService(IMapper mapper, UserManager<User> userManager, IJWTService jWTService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _jWTService = jWTService;
        }

        public async Task<AuthUserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.Users
                .SingleOrDefaultAsync(x => x.Email == loginDto.Email);   
            
            if (user == null)
            {
                throw new ArgumentException("Wrong e-mail!");
            }

            var result = await _userManager
                .CheckPasswordAsync(user, loginDto.Password);

            if (!result)
            {
                throw new Exception("Wrong password!");
            }

            var token =  await _jWTService.GenerateTokenAsync(user);

            var authUser = _mapper.Map<AuthUserDto>(user);

            authUser.Token = token;

            return authUser;
        }

        public async Task<IEnumerable<IdentityError>> RegisterAsync(RegisterDto registerDto)
        {

            var user = _mapper.Map<User>(registerDto);

            user.UserName = registerDto.Username.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Member");
            }

            return result.Errors;
        }
    }
}
