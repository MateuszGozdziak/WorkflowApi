using AutoMapper;
using WorkflowApi.Data;
using WorkflowApi.Repositories.IRepositories;

namespace WorkflowApi.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UnitOfWork(ApplicationDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        //expression-bodied members
        public IGanttEventRepository GanttEventRepository => new GanttEventRepository(_dbContext);
        public IAppRoleRepository AppRoleRepository => new AppRoleRepository(_dbContext);

        public IAppTaskRepository AppTaskRepository => new AppTaskRepository(_dbContext, _mapper);

        public IAppUserRepository AppUserRepository => new AppUserRepository(_dbContext);

        public IMessageRepository MessageRepository => new MessageRepository(_dbContext, this._mapper);

        public IPriorityRepository PriorityRepository => new PriorityRepository(_dbContext);

        public IStateRepository StateRepository => new StateRepository(_dbContext);

        public ITeamMemberRepository TeamMemberRepository => new TeamMemberRepository(_dbContext, this._mapper);

        public ITeamRepository TeamRepository => new TeamRepository(_dbContext);

        public IUserInvitedRepository UserInvitedRepository => new UserInvitedRepository(_dbContext);

        public IAppTaskAuditRepository AppTaskAuditRepository => new AppTaskAuditRepository(_dbContext, _mapper);

        public async Task<bool> Complete()
        {
            return ( await _dbContext.SaveChangesAsync() > 0 );
        }

        public bool HasChanges()
        {
            _dbContext.ChangeTracker.DetectChanges();
            var changes = _dbContext.ChangeTracker.HasChanges();

            return changes;
        }
    }
}
