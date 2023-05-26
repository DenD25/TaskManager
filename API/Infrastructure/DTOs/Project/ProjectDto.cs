using TaskManagerAPI.DTOs.Task;
using TaskManagerAPI.DTOs.User;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.DTOs.Project
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
