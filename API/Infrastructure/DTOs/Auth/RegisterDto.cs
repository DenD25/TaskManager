namespace TaskManagerAPI.DTOs.Auth
{
    public class RegisterDto: LoginDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
    }
}
