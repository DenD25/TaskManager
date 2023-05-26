namespace TaskManagerAPI.DTOs.Task
{
    public class TaskCreateDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int? AssignedToId { get; set; }
        public int ProjectId { get; set; }
    }
}
