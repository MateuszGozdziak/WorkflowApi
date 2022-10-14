using WorkflowApi.DataTransferObject;
using WorkflowApi.Entities;

namespace WorkflowApi.Repositories.IRepositories
{
    public interface IUserInvitedRepository : IRepository<UserInvited>
    {
        Task<IEnumerable<InvitedUserDto>> GetAllInvitedUsersAsync(int userId);
    }
}
