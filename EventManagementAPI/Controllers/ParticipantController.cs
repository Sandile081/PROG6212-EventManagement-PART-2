using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EventManagementAPI.Data;
using EventManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using EventManagementAPI.Helpers;

 

namespace EventManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly EventsDbContext _context;

        public ParticipantController(EventsDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Participant>> CreateParticipant(Participant participant)
        {
            participant.Ppassword = PasswordHelper.Hash(participant.Ppassword);

            _context.Participant.Add(participant);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateParticipant), new { ParticipantID = participant.ParticipantID }, participant);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participant>>> GetAllParticipants()
        {
            var FindParticipant = await _context.Participant.ToListAsync();

            return Ok(FindParticipant);
        }
        


        [HttpPut("{ParticipantID}")]
        public async Task<ActionResult<Participant>> UpdateParticipant(int ParticipantID, Participant participant)
        {
            var FindPartcipant = await _context.Participant.FindAsync(ParticipantID);

            if(FindPartcipant == null)
            {
                return NotFound();
            }

            FindPartcipant.Pname = participant.Pname;
            FindPartcipant.Psurname = participant.Psurname;
            FindPartcipant.Pusername = participant.Pusername;
            FindPartcipant.Ppassword = participant.Ppassword;

            await _context.SaveChangesAsync();
            return Ok(FindPartcipant);

        }

        [HttpGet("{ParticipantID}/EventEnrolment")]
        public async Task<ActionResult<EventEnrolment>> GetParticipantEnrolmentStatus(int ParticipantID)
        {
            var FindParticipantStatus = await _context.EventEnrollment.Where(e => e.ParticipantID == ParticipantID).FirstAsync();
          
            return Ok(FindParticipantStatus.EnrolmentStatus);
        }

        [HttpGet("{ParticipantID}/Result")]
        public async Task<ActionResult<Result>> GetParticipantResults(int ParticipantID)
        {
            var FindParticipant = await _context.Result.Where(r =>r.ParticipantID == ParticipantID).LastAsync();
            
            if (FindParticipant == null)
            {
                return NotFound();
            }

            return Ok(FindParticipant);
        }

        [HttpGet("{ParticipantID}/History")]
        public async Task<ActionResult<IEnumerable<Result>>> GetPartcipantHistory(int ParticipantID)
        {
            var FindParticipant = await _context.Result.Where(r => r.ParticipantID == ParticipantID).ToListAsync();

            if (FindParticipant == null || !FindParticipant.Any())
            {
                return NotFound();
            }

            

            return Ok(FindParticipant);

        }
    }
}
