using Infrastructure.DTOs.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagerAPI.Configurations;
using TaskManagerAPI.Contracts.Manager;
using TaskManagerAPI.Contracts.Service;
using TaskManagerAPI.DTOs.User;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Services
{
    public class JWTService : IJWTService
    {
        private readonly UserManager<User> _userManagerIdentity;
        private readonly IUserManager _userManager;
        private readonly JWTConfig _token;

        public JWTService(UserManager<User> userManagerIdentity, IOptions<JWTConfig> token, IUserManager userManager)
        {
            _userManagerIdentity = userManagerIdentity;
            _userManager = userManager;
            _token = token.Value;
        }

        public async Task<string> GenerateTokenAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var roles = await _userManagerIdentity.GetRolesAsync(user);

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_token.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Issuer = _token.Issuer,
                Audience = _token.Audience,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(_token.DurationInMinutes),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }

        public async Task<UserDto> CheckTokenAsync(string token)
        {
            var jwt = new JwtSecurityToken(token);
            if (jwt.ValidFrom > DateTime.UtcNow || jwt.ValidTo < DateTime.UtcNow)
                throw new Exception("Token is invalid");

            var userId = jwt.Claims.SingleOrDefault(x => x.Type == "nameid").Value;
            var user = await _userManager.GetUserByUserIdAsync(int.Parse(userId));
            return user;
        }
    }
}
