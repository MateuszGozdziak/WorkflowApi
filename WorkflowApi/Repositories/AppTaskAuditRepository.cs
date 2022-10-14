using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WorkflowApi.Data;
using WorkflowApi.DataTransferObject;
using WorkflowApi.Entities;
using WorkflowApi.Repositories.IRepositories;

namespace WorkflowApi.Repositories
{
    public class AppTaskAuditRepository : IAppTaskAuditRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public AppTaskAuditRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }
        public void Add(AppTaskAudit entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AppTaskAudit>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<AppTaskAudit> GetById(int Id)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<AppTaskAuditDto>> GetByTaskId(int Id)
        {
            return await _dbContext.AppTaskAudits
                .Where(t => t.EntityId == Id)
                .ProjectTo<AppTaskAuditDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public void Remove(AppTaskAudit entity)
        {
            throw new NotImplementedException();
        }
    }
}
