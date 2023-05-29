using ApplicationCore.Contracts.Service;
using Infrastructure.DTOs.Auth;
using Infrastructure.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagerAPI.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IJWTService _jWTService;
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService, IJWTService jWTService)
        {
            _authService = authService;
            _jWTService = jWTService;
        }

        // POST: api/Auth/login
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthUserDto>> Login([FromBody] LoginDto loginDto)
        {
            try{
                if (loginDto == null)
                    return Unauthorized("Form is null");

                if (loginDto.Password == "" || loginDto.Email == "")
                    return Unauthorized("Fields cannot be empty");

                var authResponse = await _authService.LoginAsync(loginDto);

                if (authResponse == null)
                {
                    return Unauthorized();
                }

                return Ok(authResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // POST: api/Auth/register
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var errors = await _authService.RegisterAsync(registerDto);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("checkToken/{token}")]
        public async Task<ActionResult<UserDto>> CheckValidTokenAsync(string token)
        {
            try
            {
                var user = await _jWTService.CheckTokenAsync(token);

                return user;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
