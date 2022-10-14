using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WorkflowApi.Data;
using WorkflowApi.DataTransferObject;
using WorkflowApi.Entities;
using WorkflowApi.Extensions;
using WorkflowApi.Repositories.IRepositories;


namespace WorkflowApi.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public MessageRepository(ApplicationDbContext dbContext,IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public void Add(Message entity)
        {
            _dbContext.Add(entity);
        }

        public async Task<IEnumerable<Message>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Message> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername)
        {
            return await _dbContext.Messages
                .Where(m => m.Recipient.UserName == currentUsername
                        && m.Sender.UserName == recipientUsername
                        || m.Recipient.UserName == recipientUsername
                        && m.Sender.UserName == currentUsername)
                //.MarkUnreadAsRead(currentUsername)
                .MarkUnreadAsRead(currentUsername)
                .OrderBy(m => m.MessageSent)
                .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public void Remove(Message entity)
        {
            throw new NotImplementedException();
        }
    }
}
