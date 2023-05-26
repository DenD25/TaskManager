namespace Infrastructure.DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public IList<string> Roles { get; set; }
    }
}
