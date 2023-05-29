using Infrastructure.Enums;

namespace Infrastructure.DTOs.Task
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public TaskStatusEnum Status { get; set; }
        public int? AssignedToId { get; set; }
        public int ProjectId { get; set; }
    }
}
