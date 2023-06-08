using Infrastructure.DTOs.Message;

namespace ApplicationCore.Contracts.Manager
{
    public interface IMessageManager
    {
        Task CreateMessage(CreateMessageDto createMessageDto);
        Task<List<MessageDto>> GetMessagesForProject(int projectId);
    }
}
