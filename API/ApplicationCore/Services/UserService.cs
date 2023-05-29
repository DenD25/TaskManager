using ApplicationCore.Contracts.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace ApplicationCore.Services
{
    public class UserService : IUserService
    {
        private readonly HttpContext _httpContext;
        private readonly IEnumerable<Claim> _user;
        public UserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext.HttpContext;
            _user = _httpContext.User.Claims;
        }

        public string GetEmail()
        {
            return _user.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value;
        }

        public string GetUserId()
        {
            return _user.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }

        public IList<string> GetUserRoles()
        {
            return _user.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList();
        }
    }
}
