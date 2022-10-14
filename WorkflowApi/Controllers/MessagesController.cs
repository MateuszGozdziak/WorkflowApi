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
using WorkflowApi.Extensions;
using WorkflowApi.Repositories.IRepositories;

namespace WorkflowApi.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class MessagesController : ControllerBase
    //{
    //    private readonly IMapper _mapper;
    //    private readonly IUnitOfWork _unitOfWork;

    //    public MessagesController(IMapper mapper,IUnitOfWork unitOfWork)
    //    {
    //        this._mapper = mapper;
    //        this._unitOfWork = unitOfWork;
    //    }

    //    // GET: api/Messages
    //    [HttpGet("GetMessages/{userId}")]
    //    public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessages(int userId)
    //    {
    //        int currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    //        var messages = _context.Messages.Where(m => (m.RecipientId == currentUserId 
    //                                                                  && m.SenderId == userId) 
    //                                                                  || (m.RecipientId == userId 
    //                                                                  && m.SenderId == currentUserId)).ToList();
          
    //        List<MessageDto> messagesDto = new List<MessageDto>();
    //        foreach (var message in messages)
    //        {
    //            messagesDto.Add(new MessageDto()
    //            {
    //                Id = message.Id,
    //                RecipientId = message.RecipientId,
    //                SenderId = message.SenderId,
    //                Content = message.Content,
    //                MessageSent = message.MessageSent//
    //            });
    //        }
    //        return messagesDto;
    //    }

    //    // POST: api/Messages
    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //    [HttpPost("PostMessage")]
    //    public async Task<IActionResult> PostMessage(MessageDto messageDto)
    //    {
    //        int currentUserId = User.GetUserId();

    //        Message message = _mapper.Map<Message>(messageDto);
    //        message.SenderId = currentUserId;

    //        _unitOfWork.MessageRepository

    //        //Message message = new()
    //        //{
    //        //    SenderId = currentUserId,
    //        //    RecipientId = messageDto.RecipientId,
    //        //    Content = messageDto.Content,
    //        //};
    //        //_context.Messages.Add(message);
    //        //await _context.SaveChangesAsync();



    //        return Ok();
    //    }

    //    // GET: api/Messages/5
    //    [HttpGet("{id}")]
    //    public async Task<ActionResult<Message>> GetMessage(int id)
    //    {
    //      if (_context.Messages == null)
    //      {
    //          return NotFound();
    //      }
    //        var message = await _context.Messages.FindAsync(id);

    //        if (message == null)
    //        {
    //            return NotFound();
    //        }

    //        return message;
    //    }

    //    // PUT: api/Messages/5
    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> PutMessage(int id, Message message)
    //    {
    //        if (id != message.Id)
    //        {
    //            return BadRequest();
    //        }

    //        _context.Entry(message).State = EntityState.Modified;

    //        try
    //        {
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!MessageExists(id))
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



    //    // DELETE: api/Messages/5
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeleteMessage(int id)
    //    {
    //        if (_context.Messages == null)
    //        {
    //            return NotFound();
    //        }
    //        var message = await _context.Messages.FindAsync(id);
    //        if (message == null)
    //        {
    //            return NotFound();
    //        }

    //        _context.Messages.Remove(message);
    //        await _context.SaveChangesAsync();

    //        return NoContent();
    //    }

    //    private bool MessageExists(int id)
    //    {
    //        return (_context.Messages?.Any(e => e.Id == id)).GetValueOrDefault();
    //    }
    //}
}
