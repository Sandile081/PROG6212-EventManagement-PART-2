using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EventManagementAPI.Data;
using EventManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EventManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventEnrolmentController : ControllerBase
    {
        private readonly EventsDbContext _context;

        public EventEnrolmentController(EventsDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<EventEnrolment>> CreateEventEnrolment(EventEnrolment eventEnrolment)
        {
            _context.EventEnrollment.Add(eventEnrolment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateEventEnrolment), new { EventEnrolmentID = eventEnrolment.EventEnrolmentID}, eventEnrolment);

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventEnrolment>>> GetAllEventEnrolment()
        {
            var FindEventEnrolment = await _context.EventEnrollment.ToListAsync();

            return Ok(FindEventEnrolment);
        }

        [HttpGet("{EventEnrolmentID}")]
        public async Task<ActionResult<EventEnrolment>> GetEventEnrolment(int EventEnrolmentID)
        {
            var FindEventEnrolment = await _context.EventEnrollment.FindAsync(EventEnrolmentID);

            if(FindEventEnrolment == null)
            {
                return BadRequest();
            }

            return Ok(FindEventEnrolment);
        }
        [HttpPut("{EventEnrolmentID}")]
        public async Task<ActionResult<EventEnrolment>> UpdateEventEnrolment(int EventEnrolmentID, EventEnrolment eventEnrolment)
        {
            var findEventEnrolment = await _context.EventEnrollment.FindAsync(EventEnrolmentID);

            if(findEventEnrolment == null)
            {
                return NotFound("Not found");
            }

            findEventEnrolment.EnrolmentStatus = eventEnrolment.EnrolmentStatus;

            await _context.SaveChangesAsync();

            return Ok(findEventEnrolment);
        }


    }
}
