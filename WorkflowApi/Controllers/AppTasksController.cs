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
using WorkflowApi.Services;
using WorkflowApi.Extensions;
using AutoMapper;
using WorkflowApi.Models;
using Newtonsoft.Json;

namespace WorkflowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppTasksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
         
        public AppTasksController(IUnitOfWork unitOfWork,IMapper mapper)
        {

            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        // GET: api/AppTasks
        [HttpGet]
        public async Task<ActionResult<SyncfusionResponse<AppTaskDto>>> Get()
        {
            var queryString = Request.Query;
            int skip = Convert.ToInt32(queryString["$skip"]);
            int take = Convert.ToInt32(queryString["$top"]);
            int teamId = Convert.ToInt32(queryString["$teamId"]);
            //teamId = Convert.ToInt32(queryString["teamId"]);
            string sort = queryString["$orderby"];//sorting   //experimental
            string filter = queryString["$filter"];//experimental

            var userId = User.GetUserId();
            var isTeamMeber = await _unitOfWork.TeamMemberRepository.GetByPrimaryKey(new TeamMember() { TeamId = teamId, UserId = userId }); 
            if (isTeamMeber == null){return Unauthorized();}

            var appTasks = await _unitOfWork.AppTaskRepository.GetAllByTeamId(teamId);
            
            //List<AppTaskDto> appTasksDto = new List<AppTaskDto>();
            //foreach (var appTask in appTasks)
            //{
            //    appTasksDto.Add(_mapper.Map<AppTaskDto>(appTask));
            //}
            //List<AppTaskDto> appTasksDto = _mapper.Map<AppTask[], List<AppTaskDto>>(appTasks);
            //List<CreateAppTaskDto> appTasksDto = _mapper.Map<AppTask[], List<CreateAppTaskDto>>(appTasks);

            //var z = from appTask in appTasksDto ;
            //appTasksDto.fo
            //appTasks.


            return Ok(new SyncfusionResponse<AppTaskDto>() { Count= appTasks.Count(), Items= appTasks.ToList() } );
        }

        // GET: api/AppTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppTaskDto>> GetPTask(int id)
        {
            var appTask = await _unitOfWork.AppTaskRepository.GetById(id);

            if (appTask == null){return NotFound();}

            return Ok(appTask);
        }

        // PUT: api/AppTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> updateAppTask(AppTaskDto[] appTaskDto)
        {//dodać sprawdzanie czy użytkownik należy do danego teamu
         //DO POPRAWY!!! 
         //https://www.entityframeworktutorial.net/entityframework6/addrange-removerange.aspx
         //https://www.syncfusion.com/forums/121424/add-multiple-new-records
            if (appTaskDto.Length == 0 || appTaskDto is null)
            {
                return NoContent();
            }

            var appTaskUpdate = _mapper.Map<AppTask[]>(appTaskDto, opt => { opt.Items["UserId"] = User.GetUserId(); });

            //var appTask = await _unitOfWork.AppTaskRepository.GetById((int)appTaskUpdateDto[0].Id);

            //if (appTask == null){return NotFound();}

            //_mapper.Map(appTaskUpdateDto, appTask);

            foreach (var item in appTaskUpdate)
            {
                _unitOfWork.AppTaskRepository.Update(item);
            }

            //_unitOfWork.AppTaskRepository.Update(appTaskUpdate);
            //var zd = _unitOfWork.HasChanges();

            if (await _unitOfWork.Complete()) {
                //Console.WriteLine(appTaskUpdate);
                return NoContent(); }

            return BadRequest("Failed to update task");
        }

        // POST: api/AppTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<object>> CreateAppTask(CreateAppTaskDto[] createAppTaskDto)
        {
            //ActionResult<SyncfusionResponse<CreateAppTaskDto>>
            //DO POPRAWY!!! MOżna zrobić ADDRANGE
            //https://www.entityframeworktutorial.net/entityframework6/addrange-removerange.aspx
            //https://www.syncfusion.com/forums/121424/add-multiple-new-records
            //_mapper.Map<AppTask[]>(appTaskDto, opt => { opt.Items["UserId"] = User.GetUserId(); });

            createAppTaskDto[0].Id = null;//inaczej identity error table 'AppTasks' when IDENTITY_INSERT is set to OFF.

            AppTask appTask = _mapper.Map<AppTask>(createAppTaskDto[0], opt => { opt.Items["UserId"] = User.GetUserId(); });
            //AppTask appTask = new AppTask() { TeamId = createAppTaskDto[0].TeamId };

            _unitOfWork.AppTaskRepository.Add(appTask);
            if (!await _unitOfWork.Complete()) {return BadRequest("Failed to Create Task");}

           var newTaskDto = _mapper.Map<CreateAppTaskDto>(appTask);
            var itemsTable = new List<CreateAppTaskDto>() { newTaskDto };
            //newTaskDto
            //int id = 2002;
            //return Ok(id);
            //new { addedRecords = z }
            //return Ok(new SyncfusionResponse<CreateAppTaskDto>() { Count = 1, Items = itemsTable });//[rzetestować
            //new { addedRecords = z }
            //return Ok(newTaskDto);
            //return Ok(new SyncfusionResponse<CreateAppTaskDto>() { Count = 1, Items = itemsTable });
            return newTaskDto;
            //return Ok(newTaskDto);
            //return CreatedAtAction("Get", new { id = newTaskDto.Id }, newTaskDto);
        }
        [HttpPost("xxtest")]
        public async Task<object> CreateAppTaskd()
        {
            //  id: 'Id',
            //name: 'Title',
            //startDate: 'StartDate',
            //endDate: 'EndDate',
            //duration: 'Duration',
            //progress: 'Progress',
            var z = new { id = 40, title = "ssss", startDate = new DateTime(), endDate = new DateTime(), duration = 1, progress = 50 };


            //return Created JsonConvert.SerializeObject(z);
            //return Ok(new {id=90});
            //return CreatedAtAction(nameof(Get), new { id = z.id }, z);
            return new { addedRecords = z };
        }
        [HttpPut("xxtest")]
        public async Task<ActionResult<SyncfusionResponse<CreateAppTaskDto>>> d()
        {
            return Ok();
        }

        // DELETE: api/AppTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppTask(int id)
        {
            
            var pTask = await _unitOfWork.AppTaskRepository.GetById(id);
            if (pTask == null){return NotFound();}

            _unitOfWork.AppTaskRepository.Remove(pTask);
            if(!await _unitOfWork.Complete()) { return BadRequest("Failed to update remove task"); }

            return NoContent();
        }
        
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<AppTaskDto>>> GetAll()
        //{
        //    var queryString = HttpContext.Request.QueryString;


        //    var appTask = await _unitOfWork.AppTaskRepository.GetAll();

        //    if (appTask == null) { return NotFound(); }

        //    IEnumerable<AppTaskDto> appTaskDto = _mapper.Map<IEnumerable<AppTaskDto>>(appTask);

        //    return Ok(appTaskDto);
        //}

        //public ActionResult UrlDatasource([FromBody] object dm)
        //{
        //    return Ok();
        //}

        //private bool PTaskExists(int id)
        //{
        //    return (_dbcontext.AppTasks?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }

}
