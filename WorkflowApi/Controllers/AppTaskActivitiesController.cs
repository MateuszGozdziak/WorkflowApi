using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkflowApi.DataTransferObject;
using WorkflowApi.Repositories.IRepositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkflowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppTaskActivitiesController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppTaskActivitiesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        // GET: api/<AppTaskActivities>
        [HttpGet("GetAllActivitiesByAppTaskId/{id}")]
        public async Task<ActionResult> GetAllActivitiesByAppTaskId(int id)
        {
            return Ok(await _unitOfWork.AppTaskAuditRepository.GetByTaskId(id));
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AppTaskActivities>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}
        //[HttpPut]
        //public async Task<IActionResult> Put(AppTaskAuditDto[] AppTaskAuditDtos)
        //{


        //    return Ok(value);   
        //}

        // DELETE api/<AppTaskActivities>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
