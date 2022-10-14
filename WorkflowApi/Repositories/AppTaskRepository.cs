using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WorkflowApi.Data;
using WorkflowApi.DataTransferObject;
using WorkflowApi.Entities;
using WorkflowApi.Models;
using WorkflowApi.Repositories.IRepositories;
using WorkflowApi.Extensions;
using System.Linq.Expressions;

namespace WorkflowApi.Repositories
{
    public class AppTaskRepository : IAppTaskRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public AppTaskRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public void Add(AppTask entity)
        {
            _dbContext.AppTasks.Add(entity);
        }

        public async Task<IEnumerable<AppTask>> GetAll()
        {
           return await _dbContext.AppTasks.ToListAsync();
        }

        public async Task<AppTask> GetById(int Id)
        {
            return await _dbContext.AppTasks.FindAsync(Id);
        }
        public async Task<AppTaskDto[]> GetAllByTeamId(int Id, AppTaskGetConfigState? state = null)
        {

            if (state == null) { 
                return await _dbContext.AppTasks
                .Where(t => t.TeamId == Id)
                .ProjectTo<AppTaskDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync();
            }
            if (state.sortName == null)
            {
                return await _dbContext.AppTasks
                .Where(t => t.TeamId == Id)
                .Skip(state.Skip)
                .Take(state.Take)
                .ProjectTo<AppTaskDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync();
            }

            return await _dbContext.AppTasks
            .Where(t => t.TeamId == Id)
            .OrderByPropertyOrField(state.sortName.FirstCharToUpper(),state.sortDirection=="ascending"? true : false)
            .Skip(state.Skip)
            .Take(state.Take)
            .ProjectTo<AppTaskDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync();
        }
        public async Task<int> CountAllByTeamId(int Id)
        {
            return await _dbContext.AppTasks.Where(t => t.TeamId == Id).CountAsync();
        }


        public void Remove(AppTask entity)
        {
            _dbContext.AppTasks.Remove(entity);
        }
        public void Update(AppTask entity)
        {
            //_dbContext.Entry(entity).State = EntityState.Modified;
            //_dbContext.Entry(entity).State = EntityState.Modified;
            //var entityEntry = _dbContext.Entry(entity);
            //var y = entityEntry.OriginalValues.Properties;
            //var i = entityEntry.CurrentValues;
            //var entityEntry = _dbContext.Entry(entity);
            //var entityEntry = _dbContext.AppTasks.Update(entity);
            var entityEntry = _dbContext.AppTasks.Update(entity);
            //var k = entityEntry.Properties.Select(x => x.o);
                //.Properties.Select(x => x.IsModified);
            //Console.WriteLine();
            // _dbContext.AppTasks.att
            //var y = z.OriginalValues["PriorityId"];
            // Console.WriteLine();
            //https://stackoverflow.com/questions/71593424/dbcontext-update-vs-entitystate-modified
            //https://stackoverflow.com/questions/34001808/ef-ismodified-false-still-updating-underlyuing-column
            //ogólnie dlatego że _dbContext.Entry(entity).State = EntityState.Modified;sprawia że wszystkie właściwością będą oznaczone jako modified nawet te które rzeczywiście nie różnią się pomiędzy bazą a przesłaną entity
        }
        public void Attach(AppTask entity)
        {
            //_dbContext.Entry(entity).State = EntityState.Modified;
            //_dbContext.Entry(entity).State = EntityState.Modified;
            //var z = _dbContext.AppTasks.Update(entity);
            //var entityEntry = _dbContext.AppTasks.Attach(entity);
            var entityEntry = _dbContext.Entry(entity);
            var y = entityEntry.OriginalValues;
            var i = entityEntry.CurrentValues;
            var k = entityEntry.Properties;
            //var y = z.OriginalValues["PriorityId"];
            //Console.WriteLine();
            //https://stackoverflow.com/questions/71593424/dbcontext-update-vs-entitystate-modified
            //https://stackoverflow.com/questions/34001808/ef-ismodified-false-still-updating-underlyuing-column
            //ogólnie dlatego że _dbContext.Entry(entity).State = EntityState.Modified;sprawia że wszystkie właściwością będą oznaczone jako modified nawet te które rzeczywiście nie różnią się pomiędzy bazą a przesłaną entity
        }
    }
}
