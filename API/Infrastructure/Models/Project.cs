﻿namespace Infrastructure.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<TaskModel>? Tasks { get; set; }
        public ICollection<ProjectUser>? Users { get; set; }
        public ICollection<Message>? Messages { get; set; }
    }
}
