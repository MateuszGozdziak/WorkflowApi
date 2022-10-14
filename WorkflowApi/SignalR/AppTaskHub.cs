using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;
using WorkflowApi.DataTransferObject;
using WorkflowApi.Entities;
using WorkflowApi.Extensions;
using WorkflowApi.Models;
using WorkflowApi.Repositories.IRepositories;

namespace WorkflowApi.SignalR
{
    [Authorize]
    public class AppTaskHub : Hub
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConnectionMultiplexer cache;
        private readonly IMapper _mapper;
        private readonly IDatabase _redisDbAsync;
        private readonly string prefix = "AppTasksHub:Team:";
        public AppTaskHub(IMapper mapper, IUnitOfWork unitOfWork, IConnectionMultiplexer cache)
        {
            _unitOfWork = unitOfWork;
            this.cache = cache;
            _mapper = mapper;
            this._redisDbAsync = cache.GetDatabase();
        }
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var teamId = httpContext.Request.Query["$teamId"].ToString();
            string userName = Context.User.GetUserNameEmail();
            int userId = Context.User.GetUserId();
            string groupName = this.prefix + teamId;

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await _redisDbAsync.HashSetAsync(groupName, Context.ConnectionId, userName);

            //var appTasks = await _unitOfWork.AppTaskRepository.GetAllByTeamId(int.Parse(teamId));
            //_mapper.m
            //List<CreateAppTaskDto> appTasksDto = _mapper.Map<AppTask[], List<AppTaskDto>>(appTasks);

            //List<AppTaskDto> appTasksDto = _mapper.Map<AppTask[]>(appTasks);
            //await Clients.Caller.SendAsync("ReceiveTasks", appTasksDto);
            //await Clients.Caller.SendAsync("ReceiveTasks", appTasks);
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var httpContext = Context.GetHttpContext();
            var teamId = httpContext.Request.Query["$teamId"].ToString();
            string userName = Context.User.GetUserNameEmail();
            int userId = Context.User.GetUserId();
            string groupName = this.prefix + teamId;

            await _redisDbAsync.HashDeleteAsync(groupName,Context.ConnectionId);
            //?????????????????czy to wystarczy żeby sam się usunął?

            await base.OnDisconnectedAsync(exception);
        }
        
        public async Task Get(AppTaskGetConfigState state)
        {
            var httpContext = Context.GetHttpContext();
            var teamId = httpContext.Request.Query["$teamId"].ToString();
            string groupName = this.prefix + teamId;
            var appTasksDto = await _unitOfWork.AppTaskRepository.GetAllByTeamId(int.Parse(teamId),state);
            var count = await _unitOfWork.AppTaskRepository.CountAllByTeamId(int.Parse(teamId));

            var appTaskSFormat = new { Result = appTasksDto, Count = count };
            await Clients.Caller.SendAsync("ReceiveTasks", appTaskSFormat);
        }
        public async Task Add(CreateAppTaskDto appTaskDto)
        {
            var httpContext = Context.GetHttpContext();
            var teamId = httpContext.Request.Query["$teamId"].ToString();
            string groupName = this.prefix + teamId;

            appTaskDto.Id = null;
            AppTask appTask = _mapper.Map<AppTask>(appTaskDto);
            _unitOfWork.AppTaskRepository.Add(appTask);
            if (!await _unitOfWork.Complete()) { throw new HubException("Failed Add Task"); }
            var newTaskDto = _mapper.Map<CreateAppTaskDto>(appTask);

            await Clients.Group(groupName).SendAsync("ReciveNewTask", newTaskDto);//group
        }
        public async Task Update(CreateAppTaskDto appTaskDto)
        {
            var httpContext = Context.GetHttpContext();
            var teamId = httpContext.Request.Query["$teamId"].ToString();
            string groupName = this.prefix + teamId;

            AppTask appTask = _mapper.Map<AppTask>(appTaskDto);

            _unitOfWork.AppTaskRepository.Update(appTask);
            if (!await _unitOfWork.Complete()) { throw new HubException("Failed to update task"); }
            var updatedTaskDto = _mapper.Map<CreateAppTaskDto>(appTask);

            await Clients.Group(groupName).SendAsync("ReciveUpdatedTask", updatedTaskDto);//group
        }
        public async Task Remove(CreateAppTaskDto appTaskDto)
        {
            var httpContext = Context.GetHttpContext();
            var teamId = httpContext.Request.Query["$teamId"].ToString();
            string groupName = this.prefix + teamId;
            AppTask appTask = _mapper.Map<AppTask>(appTaskDto);

            _unitOfWork.AppTaskRepository.Remove(appTask);
            if (!await _unitOfWork.Complete()) { throw new HubException("Failed to remove task"); }

            await Clients.Group(groupName).SendAsync("ReciveRemoveTaskId", appTask.Id);//group
        }
    }
}
