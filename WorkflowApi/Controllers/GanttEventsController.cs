using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class GanttEventsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GanttEventsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        // GET: api/GanttEvents
        [HttpGet("GetByTeamId/{id}")]
        public async Task<ActionResult<IEnumerable<GanttEventDto>>> GetByTeamId(int id)
        {
            var ganttEvents = await _unitOfWork.GanttEventRepository.GetByTeamId(id);
            if (ganttEvents == null) { return NotFound(); }
            var GanttEventsDto = _mapper.Map<IEnumerable<GanttEvent>, IEnumerable<GanttEventDto>>(ganttEvents);
            return Ok(GanttEventsDto); 
            //docelowo użyc iquwrableextensiiomapper
        }
        // POST: api/GanttEvents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GanttEventDto>> PostGanttEvent(GanttEventDto ganttEventDto)
        {
            ganttEventDto.Id = null;
            var ganttEvent = _mapper.Map<GanttEvent>(ganttEventDto);
            this._unitOfWork.GanttEventRepository.Add(ganttEvent);
            if (!await _unitOfWork.Complete()) { return BadRequest("Failed to Add Event"); }
            return Created("GetByTeamId", _mapper.Map<GanttEventDto>(ganttEvent));
            //return BadRequest("bloadd");
        }

        //return CreatedAtAction("GetGanttEvent", new { id = ganttEvent.Id }, _mapper.Map<GanttEventDto>(ganttEvent));

        // GET: api/GanttEvents/5
        ////[HttpGet("{id}")]
        ////public async Task<ActionResult<GanttEvent>> GetGanttEvent(int id)
        ////{
        ////    var ganttEvent = await _context.GanttEvents.FindAsync(id);

        ////    if (ganttEvent == null)
        ////    {
        ////        return NotFound();
        ////    }

        ////    return ganttEvent;
        ////}

        // PUT: api/GanttEvents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutGanttEvent(int id, GanttEvent ganttEvent)
        //{
        //    if (id != ganttEvent.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(ganttEvent).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!GanttEventExists(id))
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


        [HttpPost("BulkDelete")]
        public async Task<IActionResult> BulkDelete(GanttEventDto[] ganttEventsDto)
        {
            
            var ganttEvents=_mapper.Map<IEnumerable<GanttEventDto>, IEnumerable<GanttEvent>>(ganttEventsDto);
            foreach (var item in ganttEvents)
            {
                _unitOfWork.GanttEventRepository.Remove(item);
            }
            if (!await _unitOfWork.Complete()) { return BadRequest("Failed to Delete Event"); }

            return NoContent();
        }

        // DELETE: api/GanttEvents/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteGanttEvent(int id)
        //{
        //    var ganttEvent = await _context.GanttEvents.FindAsync(id);
        //    if (ganttEvent == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.GanttEvents.Remove(ganttEvent);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool GanttEventExists(int id)
        //{
        //    return _context.GanttEvents.Any(e => e.Id == id);
        //}
    }
}
