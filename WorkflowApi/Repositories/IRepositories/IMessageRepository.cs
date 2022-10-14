using WorkflowApi.DataTransferObject;
using WorkflowApi.Entities;

namespace WorkflowApi.Repositories.IRepositories
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername);
    }
}
