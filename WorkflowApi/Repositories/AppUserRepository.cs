using Microsoft.EntityFrameworkCore;
using WorkflowApi.Data;
using WorkflowApi.Entities;
using WorkflowApi.Repositories.IRepositories;

namespace WorkflowApi.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AppUserRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Add(AppUser entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AppUser>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u=>u.UserName == username);
        }

        public void Remove(AppUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
