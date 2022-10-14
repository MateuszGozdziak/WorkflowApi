using WorkflowApi.DataTransferObject;
using WorkflowApi.Entities;

namespace WorkflowApi.Repositories.IRepositories
{
    public interface ITeamMemberRepository : IRepository<TeamMember>
    {
        Task<IEnumerable<Team>> GetAllTeamForUser(int userId, int skip, int take);
        Task<TeamMember> GetByPrimaryKey(TeamMember entity);
        Task<IEnumerable<TeamMemberDto>> GetAllTeamMembers(int teamId);
        Task<IEnumerable<TeamMemberDto>> GetAllTeamMembersTEST(int teamId);
    }
}
