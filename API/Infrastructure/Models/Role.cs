using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Models
{
    public class Role : IdentityRole<int>
    {
        public ICollection<User>? Users { get; set; }
    }
}