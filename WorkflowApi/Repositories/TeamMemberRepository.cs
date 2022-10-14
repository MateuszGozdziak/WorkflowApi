using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WorkflowApi.Data;
using WorkflowApi.DataTransferObject;
using WorkflowApi.Entities;
using WorkflowApi.Repositories.IRepositories;

namespace WorkflowApi.Repositories
{
    public class TeamMemberRepository : ITeamMemberRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public TeamMemberRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }
        public void Add(TeamMember entity)
        {
            _dbContext.TeamMembers.Add(entity);
        }
        public Task<IEnumerable<TeamMember>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Team>> GetAllTeamForUser(int userId,int skip,int take)
        {
            return await _dbContext.TeamMembers
                .Skip(skip)
                .Take(take)
                .Include(t => t.Team)
                .Where(t => t.UserId == userId)
                .Select(t=>t.Team)
                .ToListAsync();
        }
        public async Task<IEnumerable<TeamMemberDto>> GetAllTeamMembers(int teamId)
        {
            return await _dbContext.TeamMembers
                .Where(t => t.TeamId == teamId)
                .Select(t => t.User)
                .ProjectTo<TeamMemberDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
        public async Task<IEnumerable<TeamMemberDto>> GetAllTeamMembersTEST(int teamId)
        {
            return await _dbContext.TeamMembers
                .Where(t => t.TeamId == teamId)
                .Select(t => t.User)
                .ProjectTo<TeamMemberDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
        //przerobić tak żeby pobrać wszystkich członków danego zespołu
        //                .Where(t => t.TeamId == teamId)
        //.Select(t => t.User)
        public async Task<TeamMember> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<TeamMember> GetByPrimaryKey(TeamMember entity)
        {
            return await _dbContext.TeamMembers.FindAsync(entity.UserId,entity.TeamId);///poprawić do get by id
            //await _dbContext.TeamMembers.FirstOrDefaultAsync(tm=>tm==entity);
        }

        public void Remove(TeamMember entity)
        {
            throw new NotImplementedException();
        }
    }
}
