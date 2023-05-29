using Infrastructure.Enums;

namespace Infrastructure.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public TaskStatusEnum Status { get; set; } = TaskStatusEnum.ToDo;
        public int? AssignedToId { get; set; }
        public User? AssignedTo { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
