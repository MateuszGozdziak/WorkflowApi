using WorkflowApi.Data;
using WorkflowApi.Entities;
using WorkflowApi.Repositories.IRepositories;

namespace WorkflowApi.Repositories
{
    public class PriorityRepository : IPriorityRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PriorityRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Add(Priority entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Priority>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Priority> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Priority entity)
        {
            throw new NotImplementedException();
        }
    }
}
