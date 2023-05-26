using TaskManagerAPI.Enums;

namespace TaskManagerAPI.Models
{
    public class ProjectUser
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ProjectUserRolesEnum Role { get; set; }
    }
}
