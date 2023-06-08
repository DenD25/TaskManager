using ApplicationCore.Contracts.Repository;
using Infrastructure.Data;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Repository
{
    public class MessageRepository: IMessageRepository
    {

        private readonly DataContext _context;
        public MessageRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Create(Message message)
        {
            await _context.Messages.AddAsync(message);
            await SaveAllAsync();
        }

        public async Task<List<Message>> GetForProject(int projectId) 
        {
            var messages = await _context.Messages
                .Include(x => x.Sender)
                .Where(x => x.ProjectId == projectId)
                .OrderBy(m => m.MessageSent)
                .ToListAsync();

            return messages;
        }

        private async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
