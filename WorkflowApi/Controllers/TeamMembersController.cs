using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkflowApi.Data;
using WorkflowApi.DataTransferObject;
using WorkflowApi.Entities;
using WorkflowApi.Repositories.IRepositories;

namespace WorkflowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMembersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeamMembersController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        // GET: api/TeamMembers
        [HttpGet("{TeamId}")]
        public async Task<ActionResult<IEnumerable<TeamMemberDto>>> GetTeamMembers(int TeamId)
        {
            return Ok(await _unitOfWork.TeamMemberRepository.GetAllTeamMembers(TeamId));
        }

        [HttpGet("TEST/{TeamId}")]
        public async Task<ActionResult<IEnumerable<TeamMemberDto>>> GetTeamMembersTEST(int TeamId)
        {
            return Ok(await _unitOfWork.TeamMemberRepository.GetAllTeamMembersTEST(TeamId));
        }
        //GetAllTeamMembersTEST
        // GET: api/TeamMembers/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<TeamMember>> GetTeamMember(int id)
        //{
        //  if (_context.TeamMembers == null)
        //  {
        //      return NotFound();
        //  }
        //    var teamMember = await _context.TeamMembers.FindAsync(id);

        //    if (teamMember == null)
        //    {
        //        return NotFound();
        //    }

        //    return teamMember;
        //}

        // PUT: api/TeamMembers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTeamMember(int id, TeamMember teamMember)
        //{
        //    if (id != teamMember.UserId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(teamMember).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TeamMemberExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/TeamMembers
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TeamMember>> PostTeamMember(TeamMember teamMember)
        //{
        //  if (_context.TeamMembers == null)
        //  {
        //      return Problem("Entity set 'ApplicationDbContext.TeamMembers'  is null.");
        //  }
        //    _context.TeamMembers.Add(teamMember);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (TeamMemberExists(teamMember.UserId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetTeamMember", new { id = teamMember.UserId }, teamMember);
        //}

        //// DELETE: api/TeamMembers/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTeamMember(int id)
        //{
        //    if (_context.TeamMembers == null)
        //    {
        //        return NotFound();
        //    }
        //    var teamMember = await _context.TeamMembers.FindAsync(id);
        //    if (teamMember == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TeamMembers.Remove(teamMember);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TeamMemberExists(int id)
        //{
        //    return (_context.TeamMembers?.Any(e => e.UserId == id)).GetValueOrDefault();
        //}
    }
}
