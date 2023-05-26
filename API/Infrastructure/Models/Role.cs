using Microsoft.AspNetCore.Identity;

namespace TaskManagerAPI.Models
{
    public class Role : IdentityRole<int>
    {
        public ICollection<User>? Users { get; set; }
    }
}