using Microsoft.EntityFrameworkCore;
using EventManagementAPI.Models;
using EventManagementAPI.Data;

namespace EventManagementAPI.Data
{
    public class EventsDbContext : DbContext
    {
       public EventsDbContext(DbContextOptions<EventsDbContext> options) : base(options)
        {

        }
       public DbSet<Events> Events { get; set; }
       public  DbSet<Organiser> Organiser { get; set; }
       public  DbSet<Participant> Participant { get; set; }
       public DbSet<Categories> Categories { get; set; }
       public DbSet<Location> Location { get; set; }
        public DbSet<EventEnrolment> EventEnrollment { get; set; }
        public DbSet<Result> Result { get; set; }
        public DbSet<History> History { get; set; }
       
    }
}
