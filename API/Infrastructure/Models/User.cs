using Infrastructure.Enums;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ProjectUserRolesEnum? ProjectRole { get; set; }
        public int? PhotoId { get; set; }
        public Photo? Photo { get; set; }
        public ICollection<Role> Roles { get; set; } 
        public ICollection<TaskModel>? Tasks { get; set; }
        public ICollection<ProjectUser>? Projects { get; set; }
        public ICollection<Message>? Messages { get; set; }
    }
}
