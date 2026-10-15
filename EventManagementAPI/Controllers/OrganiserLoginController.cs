using EventManagementAPI.Data;
using EventManagementAPI.Helpers;
using EventManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganiserLoginController : ControllerBase
    {
        private readonly EventsDbContext _context;

        public OrganiserLoginController(EventsDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<OrganiserLogin>> VerifyingOrganiser(OrganiserLogin organiserLogin)
        {
        
            var checkingUser = await _context.Organiser.FirstOrDefaultAsync(o => o.ORusername == organiserLogin.OrganiserUsername);

            

            if (checkingUser == null)
            {
                return NotFound("Access not found");
            }

            if (!PasswordHelper.Verify(organiserLogin.OrganiserPassword, checkingUser.ORpassword))
            {
                return Unauthorized("Invalid username or password");
            }

                return Ok("Access Allowed");
        }
        
    }
}
