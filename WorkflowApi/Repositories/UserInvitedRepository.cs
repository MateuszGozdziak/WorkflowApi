using Microsoft.EntityFrameworkCore;
using WorkflowApi.Data;
using WorkflowApi.Entities;
using WorkflowApi.Repositories.IRepositories;
using AutoMapper.QueryableExtensions;
using WorkflowApi.DataTransferObject;

namespace WorkflowApi.Repositories
{
    public class UserInvitedRepository : IUserInvitedRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserInvitedRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Add(UserInvited entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserInvited>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InvitedUserDto>> GetAllInvitedUsersAsync(int userId)
        {
            //zmapować to w automapper
            var invitedUsers = await _dbContext.Invitations.Where(i => i.SourceUserId == userId)
                                  .Select(i => new InvitedUserDto(){ Id = i.InvitedUserId , Email = i.InvitedUserName })
                                  .ToArrayAsync();
            var invitedByUsers = await _dbContext.Invitations.Where(i => i.InvitedUserId == userId)
                                  .Select(i => new InvitedUserDto() { Id = i.SourceUserId, Email = i.SourceUserName })
                                  .ToArrayAsync();
            var allInvitedUsers = invitedUsers.Concat(invitedByUsers);

            return allInvitedUsers;
        }

        public async Task<UserInvited> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public void Remove(UserInvited entity)
        {
            throw new NotImplementedException();
        }
    }
}
