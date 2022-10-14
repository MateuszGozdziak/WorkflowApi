using WorkflowApi.DataTransferObject;
using WorkflowApi.Entities;

namespace WorkflowApi.Repositories.IRepositories
{
    public interface IAppTaskAuditRepository : IRepository<AppTaskAudit>
    {
        Task<IEnumerable<AppTaskAuditDto>> GetByTaskId(int Id);
    }
}
