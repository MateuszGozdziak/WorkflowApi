namespace WorkflowApi.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        IAppTaskAuditRepository AppTaskAuditRepository { get; }
        IGanttEventRepository GanttEventRepository { get; }
        IAppRoleRepository AppRoleRepository { get; }
        IAppTaskRepository AppTaskRepository { get; }
        IAppUserRepository AppUserRepository { get; }
        IMessageRepository MessageRepository { get; }
        IPriorityRepository PriorityRepository { get; }
        IStateRepository StateRepository { get; }
        ITeamMemberRepository TeamMemberRepository { get; }
        ITeamRepository TeamRepository { get; }
        IUserInvitedRepository UserInvitedRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}
