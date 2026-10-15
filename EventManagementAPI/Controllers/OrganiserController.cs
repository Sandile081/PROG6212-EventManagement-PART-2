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
    public class OrganiserController : ControllerBase
    {
        private readonly EventsDbContext _context;

        public OrganiserController(EventsDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Organiser>> CreateOrganiser(Organiser organiser)
        {

            organiser.ORpassword = PasswordHelper.Hash(organiser.ORpassword);

            _context.Organiser.Add(organiser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateOrganiser), new { OrganiserID = organiser.OrganiserID}, organiser);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organiser>>> GetAllOrganiser()
        {
            var findOrganiser = await _context.Organiser.ToListAsync();

            return Ok(findOrganiser);
        }


        [HttpPut("{OrganiserID}")]
        public async Task<ActionResult<Organiser>> UpdateOrganiser(int OrganiserID , Organiser organiser)
        {
            var findOrganiser =await _context.Organiser.FindAsync(OrganiserID);

            if (findOrganiser == null)
            {
                return NotFound();
            }

            findOrganiser.ORname = organiser.ORname;
            findOrganiser.ORsurname = organiser.ORsurname;
            findOrganiser.ORusername = organiser.ORusername;
            findOrganiser.ORpassword = organiser.ORpassword;

            await _context.SaveChangesAsync();

            return Ok(findOrganiser); 
        }
        [HttpGet("{OrganiserID}/Events")]
        public async Task<ActionResult<IEnumerable<Events>>> GetOrganiserEvent(int OrganiserID)
        {
            var FindOrganiserEvent = await _context.Events.Where(e => e.OrganiserID == OrganiserID).ToListAsync();
            
            if(FindOrganiserEvent == null)
            {
                return NotFound("Event Not Found");
            }


            return Ok(FindOrganiserEvent);
        }


    }
}
