using WorkflowApi.Entities;

namespace WorkflowApi.Repositories.IRepositories
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        Task<AppUser> GetUserByUsernameAsync(string username);
    }
}
