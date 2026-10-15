using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EventManagementAPI.Models;
using EventManagementAPI.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using EventManagementAPI.Helpers;


namespace EventManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantLoginController : ControllerBase
    {
        private readonly EventsDbContext _context;

        public ParticipantLoginController(EventsDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult<ParticipantLogin>> VerifyingParticipant(ParticipantLogin participantLogin)
        {
            var chesckingUser = await _context.Participant.FirstOrDefaultAsync(p => p.Pusername == participantLogin.ParticipantUsername);

           
            if(chesckingUser == null)
            {
                return NotFound("Participant found");
            }

            if (!PasswordHelper.Verify(participantLogin.ParticipantPassword, chesckingUser.Ppassword))
            {
                return Unauthorized("Invalid username or password");
            }
             
            return Ok("Access Allowed");
        }

    }
}
