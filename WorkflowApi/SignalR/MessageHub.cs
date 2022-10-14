using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using WorkflowApi.DataTransferObject;
using WorkflowApi.Entities;
using WorkflowApi.Extensions;
using WorkflowApi.Models;
using WorkflowApi.Repositories.IRepositories;

namespace WorkflowApi.SignalR
{
    public class Group
    {
        public string GroupName { get; set; }
        public string[] ConnectionIds { get; set; }
        //connections
    }

    public class Connection
    {
        public int UserName { get; set; }
        public Group[] Groups { get; set; }
    }

    [Authorize]
    public class MessageHub : Hub
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConnectionMultiplexer cache;
        private readonly IMapper _mapper;
        private readonly IDatabase _redisDbAsync;
        private readonly string prefix="MessageHub";
        public MessageHub(IMapper mapper, IUnitOfWork unitOfWork, IConnectionMultiplexer cache)
        {
            _unitOfWork = unitOfWork;
            this.cache = cache;
            _mapper = mapper;
            this._redisDbAsync = cache.GetDatabase();
        }
        public override async Task OnConnectedAsync()
        {
            var userName = Context.User.GetUserNameEmail();
            var httpContext = Context.GetHttpContext();
            var otherUser = httpContext.Request.Query["otherUser"].ToString();
            var groupName = GetGroupName(userName, otherUser);

            List<string> groupMembers = new List<string>();
            groupMembers.Add(userName);

            var keyValues = await _redisDbAsync.HashGetAllAsync(groupName);
            if (keyValues != null)
            {
                if (keyValues.Select(v => v.Value).Contains(otherUser))
                {
                    groupMembers.Add(otherUser);
                }
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await _redisDbAsync.HashSetAsync(groupName, Context.ConnectionId, userName);

            await Clients.Group(groupName).SendAsync("UpdatedGroup", groupMembers);//group

            var messages = await _unitOfWork.MessageRepository.GetMessageThread(Context.User.GetUserNameEmail(), otherUser);

            if (_unitOfWork.HasChanges())
            {
                if(!await _unitOfWork.Complete()) { throw new HubException("Failed to mark as read"); }
            }
            await Clients.Caller.SendAsync("ReceiveMessageThread", messages);


        }
        public async Task SendMessage(MessageDto createMessageDto)
        {
            var username = Context.User.GetUserNameEmail();

            if (username == createMessageDto.RecipientUsername)
                throw new HubException("You cannot send messages to yourself");

            var sender = await _unitOfWork.AppUserRepository.GetUserByUsernameAsync(username);
            var recipient = await _unitOfWork.AppUserRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername);

            var groupName = GetGroupName(sender.UserName, recipient.UserName);

            if (recipient == null) throw new HubException("Not found user");

            var message = new Message
            {
                Sender = sender,//??
                Recipient = recipient,//??
                SenderUsername = sender.UserName,
                RecipientUsername = recipient.UserName,
                Content = createMessageDto.Content
            };

            var keyValues = await _redisDbAsync.HashGetAllAsync(groupName);
            if (keyValues != null)
            {
                if (keyValues.Select(v => v.Value).Contains(recipient.UserName))
                {
                    message.DateRead = DateTime.UtcNow;
                } 
            }

            _unitOfWork.MessageRepository.Add(message);

            if (!await _unitOfWork.Complete())
            {
                throw new HubException("Failed to add Message");
            }

            await Clients.Group(groupName).SendAsync("NewMessage", _mapper.Map<MessageDto>(message));
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userName = Context.User.GetUserNameEmail();
            var httpContext = Context.GetHttpContext();
            var otherUser = httpContext.Request.Query["otherUser"].ToString();
            var groupName = GetGroupName(userName, otherUser);
            List<string> groupMembers = new List<string>();

            await _redisDbAsync.HashDeleteAsync(groupName, Context.ConnectionId);

            var keyValues = await _redisDbAsync.HashGetAllAsync(groupName);
            if (keyValues != null)
            {
                if (keyValues.Select(v => v.Value).Contains(otherUser))
                {
                    groupMembers.Add(otherUser);
                }
                if (keyValues.Select(v => v.Value).Contains(userName))
                {
                    groupMembers.Add(userName);
                }
            }

            await Clients.Group(groupName).SendAsync("UpdatedGroup", groupMembers);//group

            await base.OnDisconnectedAsync(exception);
        }
        private string GetGroupName(string caller, string other)
        {
            var stringCompare = string.CompareOrdinal(caller, other) < 0;
            return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
        }
    }
}
