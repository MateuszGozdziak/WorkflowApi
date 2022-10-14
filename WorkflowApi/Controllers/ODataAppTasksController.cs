using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using WorkflowApi.Data;
using WorkflowApi.DataTransferObject;
using WorkflowApi.Entities;
using WorkflowApi.Repositories.IRepositories;

namespace WorkflowApi.Controllers
{
    ////public class syncfiusionTaskDto
    ////{
    ////    public int Id { get; set; }
    ////    public string? Title { get; set; }
    ////    public DateTime StartDate { get; set; } = DateTime.Now;
    ////    public DateTime EndDate { get; set; } = DateTime.Now.AddDays(7);
    ////    public double? Duration { get; set; }
    ////    public int? Progress { get; set; }
    ////    public string? Description { get; set; }
    ////    public int? ParentID { get; set; }
    ////    public bool IsParent { get; set; }
    ////    public int TeamId { get; set; }
    ////    public virtual Team Team { get; set; }
    ////}

    //[ODataRouteComponent("api/odata")]
    //[Produces("application/json")]
    //public class ODataAppTasksController : ODataController
    //{
    //    private readonly ApplicationDbContext _dbcontext;
    //    private readonly IMapper _mapper;

    //    public ODataAppTasksController(ApplicationDbContext context, IMapper mapper)
    //    {
    //        _dbcontext = context;
    //        this._mapper = mapper;
    //    }

    //    [EnableQuery]
    //    public IQueryable<syncfiusionTask> Get()
    //    {
    //        return _dbcontext.SyncfiusionTasks;
    //    }

    //    [EnableQuery]
    //    public SingleResult<syncfiusionTask> Get([FromODataUri] int key)
    //    {
    //        var result = _dbcontext.SyncfiusionTasks.Where(c => c.Id == key);
    //        return SingleResult.Create(result);
    //    }

    //    [EnableQuery]
    //    public async Task<IActionResult> Post([FromBody] syncfiusionTask syncfiusionTask)
    //    {
    //        syncfiusionTask ntask = new syncfiusionTask() { Title = "title",StartDate = DateTime.Now,EndDate= DateTime.Now ,Duration=1,Progress=1,IsParent=false,TeamId=1,Description="sdfsdf"};
    //        //syncfiusionTask.Title = "test";
    //        //syncfiusionTask.

    //        //appTaskDto.TeamId = 1;
    //        //syncfiusionTask.Id = null;
    //        // var appTask = _mapper.Map<AppTask>(appTaskDto);
    //        _dbcontext.SyncfiusionTasks.Add(ntask);
    //        await _dbcontext.SaveChangesAsync();

    //        //return Ok(appTask);
    //        //return appTask;
    //        //return CreatedODataResult<appTask>
    //        //return Request.CreateResponse(HttpStatusCode.OK, value);
    //        //return Created(appTask);
    //        //return Json(value);
    //        return CreatedAtAction("Get", new { id = ntask.Id }, ntask);
    //        //return Created(ntask);
    //    }

    //    [EnableQuery]
    //    public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] syncfiusionTask appTaskDto)
    //    {
    //        //if (!ModelState.IsValid)
    //        //{
    //        //    return BadRequest(ModelState);
    //        //}
    //        var appTask = _mapper.Map<AppTask>(appTaskDto);

    //        var existingNote = await _dbcontext.AppTasks.FindAsync(key);
    //        if (existingNote == null)
    //        {
    //            return NotFound();
    //        }
    //        _dbcontext.Entry(appTask).State = EntityState.Modified;
    //        //appTask.Patch(existingNote);
    //        try
    //        {
    //            await _dbcontext.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!NoteExists(key))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }
    //        return Updated(existingNote);
    //    }

    //    //[EnableQuery]
    //    //public async Task<IActionResult> Patch([FromODataUri] int key, Delta<AppTask> appTask)
    //    //{
    //    //    if (!ModelState.IsValid)
    //    //    {
    //    //        return BadRequest(ModelState);
    //    //    }
    //    //    var existingNote = await _dbcontext.AppTasks.FindAsync(key);
    //    //    if (existingNote == null)
    //    //    {
    //    //        return NotFound();
    //    //    }

    //    //    appTask.Patch(existingNote);
    //    //    try
    //    //    {
    //    //        await _dbcontext.SaveChangesAsync();
    //    //    }
    //    //    catch (DbUpdateConcurrencyException)
    //    //    {
    //    //        if (!NoteExists(key))
    //    //        {
    //    //            return NotFound();
    //    //        }
    //    //        else
    //    //        {
    //    //            throw;
    //    //        }
    //    //    }
    //    //    return Updated(existingNote);
    //    //}

    //    [EnableQuery]
    //    public async Task<IActionResult> Delete([FromODataUri] int key)
    //    {
    //        syncfiusionTask existingNote = await _dbcontext.SyncfiusionTasks.FindAsync(key);
    //        if (existingNote == null)
    //        {
    //            return NotFound();
    //        }

    //        _dbcontext.SyncfiusionTasks.Remove(existingNote);
    //        await _dbcontext.SaveChangesAsync();
    //        return StatusCode(StatusCodes.Status204NoContent);
    //    }

    //    private bool NoteExists(int key)
    //    {
    //        return _dbcontext.SyncfiusionTasks.Any(p => p.Id == key);
    //    }

    //}
}
