using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkflowApi.Data;
using WorkflowApi.Entities;
using WorkflowApi.Extensions;
using WorkflowApi.Repositories;

namespace WorkflowApi.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class SyncfiusionTasksController : ControllerBase
    //{
    //    private readonly ApplicationDbContext _context;
    //    //private readonly UnitOfWork _unitOfWork;

    //    public SyncfiusionTasksController(ApplicationDbContext context)
    //    {
    //        this._context = context;
    //        //this._unitOfWork = unitOfWork;
    //    }

    //    [HttpGet("GetByTeamId/{id}")]
    //    public async Task<ActionResult<IEnumerable<syncfiusionTask>>> GetAllByTeamId(int id)
    //    {//dto
    //        //var userId = User.GetUserId();
    //        //var isTeamMeber = await _unitOfWork.TeamMemberRepository.GetByPrimaryKey(new TeamMember() { TeamId = id, UserId = userId });
    //        //if (isTeamMeber == null) { return Unauthorized(); }


    //        var SyncfiusionTasks = await _context.SyncfiusionTasks.Where(t => t.TeamId == id).ToArrayAsync();
    //        //var appTasks = await _unitOfWork.AppTaskRepository.GetByTeamId(id);
    //        //List<AppTaskDto> appTasksDto = _mapper.Map<AppTask[], List<AppTaskDto>>(appTasks);

    //        return Ok(SyncfiusionTasks);
    //    }

    //    // GET: api/SyncfiusionTasks
    //    [HttpGet]
    //    public async Task<ActionResult<IEnumerable<syncfiusionTask>>> GetSyncfiusionTasks()
    //    {
    //      if (_context.SyncfiusionTasks == null)
    //      {
    //          return NotFound();
    //      }
    //        return await _context.SyncfiusionTasks.ToListAsync();
    //    }

    //    // GET: api/SyncfiusionTasks/5
    //    [HttpGet("{id}")]
    //    public async Task<ActionResult<syncfiusionTask>> GetsyncfiusionTask(int id)
    //    {
    //      if (_context.SyncfiusionTasks == null)
    //      {
    //          return NotFound();
    //      }
    //        var syncfiusionTask = await _context.SyncfiusionTasks.FindAsync(id);

    //        if (syncfiusionTask == null)
    //        {
    //            return NotFound();
    //        }

    //        return syncfiusionTask;
    //    }

    //    // PUT: api/SyncfiusionTasks/5
    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> PutsyncfiusionTask(int id, syncfiusionTask syncfiusionTask)
    //    {
    //        if (id != syncfiusionTask.Id)
    //        {
    //            return BadRequest();
    //        }

    //        _context.Entry(syncfiusionTask).State = EntityState.Modified;

    //        try
    //        {
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!syncfiusionTaskExists(id))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }

    //        return NoContent();
    //    }

    //    // POST: api/SyncfiusionTasks
    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //    [HttpPost]
    //    public async Task<ActionResult<syncfiusionTask>> PostsyncfiusionTask(syncfiusionTask syncfiusionTask)
    //    {
    //      if (_context.SyncfiusionTasks == null)
    //      {
    //          return Problem("Entity set 'ApplicationDbContext.SyncfiusionTasks'  is null.");
    //      }
    //        _context.SyncfiusionTasks.Add(syncfiusionTask);
    //        await _context.SaveChangesAsync();

    //        return CreatedAtAction("GetsyncfiusionTask", new { id = syncfiusionTask.Id }, syncfiusionTask);
    //    }

    //    // DELETE: api/SyncfiusionTasks/5
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeletesyncfiusionTask(int id)
    //    {
    //        if (_context.SyncfiusionTasks == null)
    //        {
    //            return NotFound();
    //        }
    //        var syncfiusionTask = await _context.SyncfiusionTasks.FindAsync(id);
    //        if (syncfiusionTask == null)
    //        {
    //            return NotFound();
    //        }

    //        _context.SyncfiusionTasks.Remove(syncfiusionTask);
    //        await _context.SaveChangesAsync();

    //        return NoContent();
    //    }

    //    private bool syncfiusionTaskExists(int id)
    //    {
    //        return (_context.SyncfiusionTasks?.Any(e => e.Id == id)).GetValueOrDefault();
    //    }
    //}
}
