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
    public class LocationController : ControllerBase
    {
        private readonly EventsDbContext _context;

        public LocationController(EventsDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Location>> CreateLocation(Location location)
        {
            _context.Location.Add(location);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateLocation), new { LocationID = location.LocationID }, location);

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>>  GetAllLocations()
        {
            var FindLocation = await _context.Location.ToListAsync();
            return Ok(FindLocation);
        }

        [HttpGet("{locationID}")]
        public async Task<ActionResult<Location>> GetLocation(int locationID)
        {
            var FindLocation = await _context.Location.FindAsync(locationID);

            if(FindLocation == null)
            {
                return NotFound();
            }

            return Ok(FindLocation);
        }

        [HttpPut("{LocationID}")]
        public async Task<ActionResult<Location>> UpdateLocation(int LocationID, Location location)
        {
            var FindLocation = await _context.Location.FindAsync(LocationID);

            if(FindLocation == null)
            {
                return NotFound();
            }

            FindLocation.Province = location.Province;
            FindLocation.Town = location.Town;
            FindLocation.PhysicalAddress = location.PhysicalAddress;

            await _context.SaveChangesAsync();
            return Ok(FindLocation);
        }

    }
}
