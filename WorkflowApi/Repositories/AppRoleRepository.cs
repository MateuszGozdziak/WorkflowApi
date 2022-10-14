using WorkflowApi.Data;
using WorkflowApi.Entities;
using WorkflowApi.Repositories.IRepositories;

namespace WorkflowApi.Repositories
{
    public class AppRoleRepository : IAppRoleRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AppRoleRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Add(AppRole entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AppRole>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<AppRole> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public void Remove(AppRole entity)
        {
            throw new NotImplementedException();
        }
    }
}
