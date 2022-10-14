using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkflowApi.Data;
using WorkflowApi.DataTransferObject;
using WorkflowApi.Entities;
using WorkflowApi.Services;
using WorkflowApi.Extensions;
using WorkflowApi.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;
using WorkflowApi.Models;

namespace WorkflowApi.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TeamsController> _logger;
        private readonly IMapper _mapper;

        public TeamsController(IUnitOfWork unitOfWork, ILogger<TeamsController> logger, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
            this._mapper = mapper;
        }


        //public async Task<ActionResult<IEnumerable<TeamDto>>> Get()

        // GET: api/Teams
        [HttpGet]
        public async Task<object> Get()
        {
            var queryString = Request.Query;
            int skip = Convert.ToInt32(queryString["$skip"]);
            int take = Convert.ToInt32(queryString["$top"]);
            string sort = queryString["$orderby"];//sorting   //experimental
            string filter = queryString["$filter"];//experimental



            int userId = User.GetUserId();
            var belongingsTeams = await _unitOfWork.TeamMemberRepository.GetAllTeamForUser(userId,skip,take);
            if (belongingsTeams == null) { return NotFound(); }

            var teamDtoList = _mapper.Map<IEnumerable<Team>, List<TeamDto>>(belongingsTeams);
            //return new {Items = teamDtoList.ToArray(), Count = teamDtoList.Count()};
            return new SyncfusionResponse<TeamDto>() {Count= teamDtoList.Count(), Items = teamDtoList} ;
        }

        // GET: api/Teams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDto>> GetOne(int id)
        {
            int userId = User.GetUserId();

            var isMember = await _unitOfWork.TeamMemberRepository.GetByPrimaryKey(new TeamMember { TeamId = id, UserId = userId });
            if (isMember == null) { return Unauthorized(); }

            var team = await _unitOfWork.TeamRepository.GetById(id);
            if (team == null) { return NotFound(); }

            var teamDto = _mapper.Map<TeamDto>(team);

            return teamDto;
        }

        // PUT: api/Teams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateTeam(int id, Team team)
        //{
        //    if (id != team.Id)
        //    {
        //        return BadRequest();
        //    }
        //    _context.Entry(team).State = EntityState.Modified;
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TeamExists(id))
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

        // POST: api/Teams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpGet("Test")]
        [HttpPost]
        public async Task<ActionResult<TeamDto>> CreateTeam(CreateTeamDto createTeamDto)
        {
            int userId = User.GetUserId();

            var team = new Team() { Name = createTeamDto.Name };
            _unitOfWork.TeamRepository.Add(team);

            if (!await _unitOfWork.Complete()) { return BadRequest("Failed to create Team"); }

            TeamMember teamMember = new TeamMember()
            { TeamId = team.Id, UserId = userId };

            _unitOfWork.TeamMemberRepository.Add(teamMember);

            if (!await _unitOfWork.Complete()) { return BadRequest("Failed to create TeamMember"); }

            TeamDto teamDboRet = _mapper.Map<TeamDto>(team);

            return Ok(teamDboRet);
        }

        // DELETE: api/Teams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            int userId = User.GetUserId();

            var isMember = _unitOfWork.TeamMemberRepository.GetByPrimaryKey(new TeamMember { TeamId = id, UserId = userId });
            if (isMember == null) { return Unauthorized(); }

            var team = await _unitOfWork.TeamRepository.GetById(id);
            if (team == null) { return NotFound(); }

            _unitOfWork.TeamRepository.Remove(team);

            if (await _unitOfWork.Complete()) { return NoContent(); }

            return BadRequest("Failed to update task");
        }

        //private bool TeamExists(int id)
        //{
        //    return (_context.Teams?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}