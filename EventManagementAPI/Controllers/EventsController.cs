using EventManagementAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using EventManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EventManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly EventsDbContext _context;

        public EventsController(EventsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Events>>> GetEvents()
        {
            return await _context.Events.ToListAsync();
        }
        [HttpGet("{EventID}")]
        public async Task<ActionResult<Events>> GetSingleEvent(int EventID)
        {
          var FindEvent = await _context.Events.FindAsync(EventID);

            if(FindEvent == null)
            {
                return NotFound();
            }

            return Ok(FindEvent);
        }

        [HttpPost]
        public async Task<ActionResult<Events>> CreateEvents(Events Events)
        {
            _context.Events.Add(Events);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateEvents), new { EventID = Events.EventID }, Events);
        }

        [HttpPut("{EventID}")]
        public async Task<IActionResult> Update(int EventID, Events Event)
        {
            if(EventID != Event.EventID)
            {
                return BadRequest("The ID does not match");
            }
            var FindEvent = await _context.Events.FindAsync(EventID);

            if(FindEvent == null)
            {
                return NotFound();
            }

            FindEvent.EventName = Event.EventName;
            FindEvent.Description = Event.Description;
            FindEvent.Date = Event.Date;
            FindEvent.Distance = Event.Distance;
            FindEvent.EventType = Event.EventType;
            FindEvent.OrganiserID = Event.OrganiserID;
            FindEvent.CategoryID = Event.CategoryID;
            FindEvent.LocationID = Event.LocationID;

            await _context.SaveChangesAsync();
            return Ok(Event);
        }

        [HttpDelete("{EventID}")]
        public async Task<IActionResult> DeleteEvents(int EventID)
        {
          

            var FindEvent = await _context.Events.FindAsync(EventID);

            if(FindEvent == null)
            {
                return NotFound();
            }

            _context.Events.Remove(FindEvent);
            await _context.SaveChangesAsync();
            return Ok();
        }



    }
}
