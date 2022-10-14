using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using System.Security.Claims;
using WorkflowApi.Data;
using WorkflowApi.Models;
using WorkflowApi.Extensions;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Text.Json;
using WorkflowApi.DataTransferObject;
using WorkflowApi.Repositories.IRepositories;

namespace WorkflowApi.SignalR
{
    public class UserState
    {
        public string UserName { get; set; }
        public Task<bool> IsOnline { get; set; }
    }

    [Authorize]
    public class PresenceHub : Hub
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConnectionMultiplexer _cache;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDatabase _redisDbAsync;
        public PresenceHub(ApplicationDbContext dbContext, IConnectionMultiplexer cache, IUnitOfWork unitOfWork)
        {
            this._dbContext = dbContext;
            this._redisDbAsync = cache.GetDatabase();
            this._cache = cache;
            this._unitOfWork = unitOfWork;
        }

        public override async Task OnConnectedAsync()
        {//do poprawy
            string userName = Context.User.GetUserNameEmail();
            int userId = Context.User.GetUserId();

            await _redisDbAsync.SetAddAsync(userName, Context.ConnectionId);            
            string[] InvitedUsersConnectionIds = await GetInvitedUsers();
            await Clients.Clients(InvitedUsersConnectionIds).SendAsync("UserIsOnline",userName);

            var currentUsers = await GetOnlineUsers();
            await Clients.Caller.SendAsync("GetOnlineUsers", currentUsers);

            await base.OnConnectedAsync();
        }

        private async Task<List<string>> GetOnlineUsers()
        {
            var allInvitedUsers = await _unitOfWork.UserInvitedRepository.GetAllInvitedUsersAsync(Context.User.GetUserId());
            var allInvitedNames = allInvitedUsers.Select(x => x.Email).ToArray();

            List<UserState> userState = new List<UserState>();
            foreach (var item in allInvitedNames)
            {
                Task<bool> keyExists = _redisDbAsync.KeyExistsAsync(item);
                userState.Add(new UserState { IsOnline = keyExists, UserName= item });
            }

            List<string> onlineUserNames = new();
            foreach (var item in userState)
            {
                if (await item.IsOnline)
                {
                    onlineUserNames.Add(item.UserName);
                } 
            }

            return onlineUserNames;
        }

        private async Task<string[]> GetInvitedUsers()
        {
            int userId = Context.User.GetUserId();

            var invitedUsers = _dbContext.Invitations.Where(i => i.SourceUserId == userId)
                                  .Include(i => i.InvitedUser)
                                  .Select(i => i.InvitedUser);
            var invitedByUsers = _dbContext.Invitations.Where(i => i.InvitedUserId == userId)
                                  .Include(i => i.SourceUser)
                                  .Select(i => i.SourceUser);
            var allInvitedUsersName = invitedUsers.Concat(invitedByUsers)
                                  .Select(u => u.Email)
                                  .ToArray();

            List<Task<RedisValue[]>> taskListOfSesionIds = new List<Task<RedisValue[]>>();
            var transaction = _redisDbAsync.CreateTransaction();

            foreach (var item in allInvitedUsersName)
            {
                taskListOfSesionIds.Add(transaction.SetMembersAsync(item));
            };
            bool operationState = transaction.Execute();

            if (!operationState) { throw new Exception("GetInvitedUsers error"); }

            var SesionIds = await Task.WhenAll(taskListOfSesionIds);
            var flatenSesionIds = SesionIds.SelectMany(s => s).Select(z => z.ToString()).ToArray();

            return flatenSesionIds;
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string userName = Context.User.GetUserNameEmail();
            int userId = Context.User.GetUserId();

            await _redisDbAsync.SetRemoveAsync(userName, Context.ConnectionId);
            var ConnectionLength = await _redisDbAsync.SetLengthAsync(userName);

            if (ConnectionLength == 0)
            {
                await _redisDbAsync.KeyDeleteAsync(userName);
                string[] InvitedUsersConnectionIds = await GetInvitedUsers();
                await Clients.Clients(InvitedUsersConnectionIds).SendAsync("UserIsOffline",userName);
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendInvitation()
        {
            //todo
        }
    }
}
