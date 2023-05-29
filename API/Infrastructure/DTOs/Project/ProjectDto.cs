using Infrastructure.DTOs.Task;
using Infrastructure.DTOs.User;

namespace Infrastructure.DTOs.Project
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public ICollection<TaskDto> Tasks { get; set; }
        public ICollection<UserProjectDto> Users { get; set; }
    }
}
