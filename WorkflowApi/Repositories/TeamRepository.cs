using Microsoft.EntityFrameworkCore;
using WorkflowApi.Data;
using WorkflowApi.Entities;
using WorkflowApi.Repositories.IRepositories;

namespace WorkflowApi.Repositories
{
    public class TeamRepository : ITeamRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public TeamRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Add(Team entity)
        {
            _dbContext.Teams.Add(entity);   
        }

        public async Task<IEnumerable<Team>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Team> GetById(int Id)
        {
            return await _dbContext.Teams.FindAsync(Id);
        }

        public void Remove(Team entity)
        {
            _dbContext.Teams.Remove(entity);
        }
    }
}
