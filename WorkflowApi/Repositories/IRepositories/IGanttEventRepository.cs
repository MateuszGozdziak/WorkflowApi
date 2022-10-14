using WorkflowApi.Entities;

namespace WorkflowApi.Repositories.IRepositories
{
    public interface IGanttEventRepository : IRepository<GanttEvent>
    {
        Task<IEnumerable<GanttEvent>> GetByTeamId(int teamId);

    }
}
