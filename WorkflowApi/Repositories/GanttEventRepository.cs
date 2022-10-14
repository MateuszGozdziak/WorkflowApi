using Microsoft.EntityFrameworkCore;
using WorkflowApi.Data;
using WorkflowApi.Entities;
using WorkflowApi.Repositories.IRepositories;

namespace WorkflowApi.Repositories
{
    public class GanttEventRepository : IGanttEventRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public GanttEventRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Add(GanttEvent entity)
        {
            _dbContext.GanttEvents.Add(entity);
        }

        public Task<IEnumerable<GanttEvent>> GetAll()
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<GanttEvent>> GetByTeamId(int teamId)
        {
            return await _dbContext.GanttEvents.Where(g => g.TeamId == teamId).ToListAsync();
        }
        public async Task<GanttEvent> GetById(int Id)
        {
            return await _dbContext.GanttEvents.FindAsync(Id); //<--- to sie wydaje lepszym rozwiązaniem
            //return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username); <--niż to
        }
        public void Remove(GanttEvent entity)
        {
            _dbContext.GanttEvents.Remove(entity);
        }
    }
}
