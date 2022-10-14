using WorkflowApi.DataTransferObject;
using WorkflowApi.Entities;
using WorkflowApi.Models;

namespace WorkflowApi.Repositories.IRepositories
{
    public interface IAppTaskRepository : IRepository<AppTask>
    {
        //Task<AppTask[]> GetAllByTeamId(int Id);
        Task<AppTaskDto[]> GetAllByTeamId(int Id, AppTaskGetConfigState? state = null);
        Task<int> CountAllByTeamId(int Id);
        void Update(AppTask entity);
    }
}
