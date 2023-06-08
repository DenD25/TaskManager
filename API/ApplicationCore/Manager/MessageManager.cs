using ApplicationCore.Contracts.Manager;
using ApplicationCore.Contracts.Repository;
using AutoMapper;
using Infrastructure.DTOs.Message;
using Infrastructure.Models;

namespace ApplicationCore.Manager
{
    public class MessageManager : IMessageManager
    {
        private readonly IMapper _mapper;
        private readonly IMessageRepository _messageRepository;

        public MessageManager(IMapper mapper ,IMessageRepository messageRepository) 
        {
            _mapper = mapper;
            _messageRepository = messageRepository;
        }

        public async Task CreateMessage(CreateMessageDto createMessageDto)
        {
            var message = _mapper.Map<Message>(createMessageDto);

            await _messageRepository.Create(message);
        }

        public async Task<List<MessageDto>> GetMessagesForProject(int projectId)
        {
            var messages = await _messageRepository.GetForProject(projectId);

            var messagesDto = _mapper.Map<List<MessageDto>>(messages);

            return messagesDto;
        }
    }
}
