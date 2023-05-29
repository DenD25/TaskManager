namespace Infrastructure.DTOs.Auth
{
    public class AuthUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string? PhotoUrl { get; set; }
        public string Token { get; set; }
    }
}
