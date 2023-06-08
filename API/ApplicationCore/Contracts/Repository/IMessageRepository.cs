using Infrastructure.Models;

namespace ApplicationCore.Contracts.Repository
{
    public interface IMessageRepository
    {
        Task Create(Message message);
        Task<List<Message>> GetForProject(int projectId);
    }
}
