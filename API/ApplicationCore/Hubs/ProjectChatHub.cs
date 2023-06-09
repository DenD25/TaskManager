using ApplicationCore.Contracts.Manager;
using ApplicationCore.Contracts.Service;
using Infrastructure.DTOs.Message;
using Microsoft.AspNetCore.SignalR;

namespace ApplicationCore.Hubs
{
    public class ProjectChatHub : Hub
    {

        private readonly IUserService _userService;
        private readonly IUserManager _userManager;
        private readonly IProjectManager _projectManager;
        private readonly IMessageManager _messageManager;

        public ProjectChatHub(IUserService userService, IUserManager userManager, IProjectManager projectManager, IMessageManager messageManager)
        {
            _userService = userService;
            _userManager = userManager;
            _projectManager = projectManager;
            _messageManager = messageManager;
        }

        public override async Task OnConnectedAsync()
        {

            var projectId = Context.GetHttpContext().Request.Query["projectId"].ToString();

            var userExist = await _projectManager.GetUserExistAsync(int.Parse(projectId), null);

            if (userExist == false)
                throw new HubException("User is not a member of the project");

            await Groups.AddToGroupAsync(Context.ConnectionId, projectId);

            var messages = await _messageManager.GetMessagesForProject(int.Parse(projectId));

            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessageThread", messages);

            await base.OnConnectedAsync();
        }

        public async Task SendMessage(CreateMessageDto createMessageDto)
        {

            var user = await _userManager.GetUserByUserIdAsync(createMessageDto.SenderId);

            await _messageManager.CreateMessage(createMessageDto);

            var messageModel = new MessageDto
            {
                ProjectId = createMessageDto.ProjectId,
                SenderId = createMessageDto.SenderId,
                SenderPhotoUrl = user.PhotoUrl,
                SenderUsername = user.UserName,
                Content = createMessageDto.Content
            };

            await Clients.Group(messageModel.ProjectId.ToString()).SendAsync("SendMessage", messageModel);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var projectId = Context.GetHttpContext().Request.Query["projectId"].ToString();

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, projectId);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
