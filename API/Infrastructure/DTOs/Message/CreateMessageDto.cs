namespace Infrastructure.DTOs.Message
{
    public class CreateMessageDto
    {
        public int ProjectId { get; set; }
        public int SenderId { get; set; }
        public string Content { get; set; }
    }
}
