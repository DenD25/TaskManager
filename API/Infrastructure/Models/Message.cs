namespace Infrastructure.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public string Content { get; set; }
        public DateTime MessageSent { get; set; } = DateTime.UtcNow;
    }
}
