using EventManagementAPI.Data;
using EventManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly EventsDbContext _context;

        public HistoryController(EventsDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<History>> CreateEventEnrolment(History history)
        {
            _context.History.Add(history);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateEventEnrolment), new { HistoryID = history.HistoryID }, history);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<History>>> GetAllHistory()
        {
            var FindHistory = await _context.History.ToListAsync();
            return Ok(FindHistory);
        }
    }
}
