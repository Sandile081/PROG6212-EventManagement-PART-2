using EventManagementAPI.Data;
using EventManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly EventsDbContext _context;

        public ResultsController(EventsDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Result>> CreateResults(Result result)
        {
            _context.Result.Add(result);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateResults), new { ResultID = result.ResultID}, result);
        }

        [HttpGet("{ResultID}")]
        public async Task<ActionResult<Result>> GetSpecificResult(int ResultID)
        {

            var findResults = _context.Result.FindAsync(ResultID);

            if(findResults == null)
            {
                return NotFound();
            }

            return Ok(findResults);
        }

        [HttpPut("{ResultID}")]
        public async Task<ActionResult<Result>> UpdateResults(int ResultID, Result result)
        {
            var FindResults = await _context.Result.FindAsync(ResultID);

            if(FindResults == null)
            {
                return NotFound();
            }

            FindResults.Time = result.Time;
            FindResults.FinishingPosition = result.FinishingPosition;
            FindResults.OrganiserID = result.OrganiserID;
            FindResults.ParticipantID = result.ParticipantID;

            await _context.SaveChangesAsync();

            return Ok(FindResults);

        }

        


    }
}
