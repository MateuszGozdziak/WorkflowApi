using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkflowApi.Data;
using WorkflowApi.DataTransferObject;
using WorkflowApi.Extensions;
using WorkflowApi.Entities;
using WorkflowApi.Repositories.IRepositories;

namespace WorkflowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public InvitationsController(ApplicationDbContext context,UserManager<AppUser> userManager,IUnitOfWork unitOfWork)
        {
            this._context = context;
            this._userManager = userManager;
            this._unitOfWork = unitOfWork;
        }

        // GET: api/Invitations/5
        [HttpGet("FindUser/{email}")]
        public async Task<ActionResult<List<InvitedUserDto>>> FindUser(string email)
        {
            if(email==null)
            {
                return BadRequest();
            }
            var users = _userManager.Users.Where(u => u.Email.StartsWith(email)).ToList();

            List<InvitedUserDto> foundUsers = new List<InvitedUserDto>();
            foreach (var user in users)
            {
                foundUsers.Add(new InvitedUserDto() { Email = user.Email,Id=user.Id });
            };

            return foundUsers;
        }

        // GET: api/Invitations
        [HttpGet("GetAllInvitedUsers")]
        public async Task<ActionResult<List<InvitedUserDto>>> GetAllInvitedUsers()
        {
            var userId = User.GetUserId();

            var allInvitedUsersName = await _unitOfWork.UserInvitedRepository.GetAllInvitedUsersAsync(userId);
            var c=allInvitedUsersName.ToList();
            //
            //var usersList = _context.Invitations
            //    .Include(i => i.InvitedUser)
            //    .Where(i => i.SourceUserId == userId &&  i.InvitedUserId == userId);

            //List<InvitedUserDto> foundUsers = new List<InvitedUserDto>();
            //foreach (var user in usersList)
            //{
            //    foundUsers.Add(new InvitedUserDto() { Email = user.InvitedUser.Email, Id=user.InvitedUserId });
            //};

            return c;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("InviteUser")]
        public async Task<ActionResult> PostInviteUser(InvitedUserDto invitedUserDto)
        {
            var sourseUserId = User.GetUserId();

            var Invitation = _context.Invitations.FirstOrDefault(i =>
                (i.InvitedUserId == invitedUserDto.Id && i.SourceUserId == sourseUserId)
                || (i.InvitedUserId == sourseUserId && i.SourceUserId == invitedUserDto.Id));

            if (Invitation != null)
            {
                return BadRequest("This invitation is already exist.");
            }

            UserInvited userInvited = new UserInvited()
            {
                SourceUserName = User.GetUserNameEmail(),
                SourceUserId = sourseUserId,
                InvitedUserId = invitedUserDto.Id,
                InvitedUserName = invitedUserDto.Email
            };

            _context.Invitations.Add(userInvited);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool UserInvitedExists(int id)
        {
            return (_context.Invitations?.Any(e => e.SourceUserId == id)).GetValueOrDefault();
        }
    }
}
