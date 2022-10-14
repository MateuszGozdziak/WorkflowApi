using WorkflowApi.Data;
using WorkflowApi.Entities;
using WorkflowApi.Repositories.IRepositories;

namespace WorkflowApi.Repositories
{
    public class StateRepository : IStateRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StateRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Add(State entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<State>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<State> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public void Remove(State entity)
        {
            throw new NotImplementedException();
        }
    }
}
