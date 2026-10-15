using EventManagementAPI.Controllers;
using EventManagementAPI.Data;
using EventManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EventManagementAPI.tests
{
    public class EventsControllerTests
    {
       
        private EventsDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<EventsDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new EventsDbContext(options);
        }

        
        private async Task<Events> SeedEventAsync(EventsDbContext context, int id = 1)
        {
            var evt = new Events
            {
                EventID = id,
                EventName = "Test Event",
                Description = "A test event",
                Date = "2025-01-01",
                Distance = "10km",
                EventType = "Running",
                OrganiserID = 1,
                CategoryID = 1,
                LocationID = 1
            };
            context.Events.Add(evt);
            await context.SaveChangesAsync();
            return evt;
        }

 
        [Fact]
        public async Task GetEvents_ReturnsEmptyList_WhenNoEventsExist()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new EventsController(context);

            // Act
            var result = await controller.GetEvents();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Events>>>(result);
            var events = Assert.IsType<List<Events>>(actionResult.Value);
            Assert.Empty(events);
        }

        [Fact]
        public async Task GetEvents_ReturnsAllEvents_WhenEventsExist()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            await SeedEventAsync(context, 1);
            await SeedEventAsync(context, 2);
            var controller = new EventsController(context);

            // Act
            var result = await controller.GetEvents();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Events>>>(result);
            var events = Assert.IsType<List<Events>>(actionResult.Value);
            Assert.Equal(2, events.Count);
        }


        // GET: api/events/{id}

        [Fact]
        public async Task GetSingleEvent_ReturnsEvent_WhenEventExists()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            await SeedEventAsync(context, 5);
            var controller = new EventsController(context);

            // Act
            var result = await controller.GetSingleEvent(5);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Events>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var evt = Assert.IsType<Events>(okResult.Value);  

            Assert.Equal(5, evt.EventID);
            Assert.Equal("Test Event", evt.EventName);
        }

        [Fact]
        public async Task GetSingleEvent_ReturnsNotFound_WhenEventDoesNotExist()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new EventsController(context);

            // Act
            var result = await controller.GetSingleEvent(999);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Events>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);   
        }


        // POST: api/events

        [Fact]
        public async Task CreateEvents_AddsEvent_AndReturnsCreatedAtAction()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new EventsController(context);

            var newEvent = new Events
            {
                EventID = 10,
                EventName = "New Marathon",
                Description = "A brand new marathon",
                Date = "2025-06-15",
                Distance = "42km",
                EventType = "Marathon",
                OrganiserID = 1,
                CategoryID = 2,
                LocationID = 3
            };

            // Act
            var result = await controller.CreateEvents(newEvent);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Events>>(result);
            var createdResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);

            // Verify it was saved to the database
            var savedEvent = await context.Events.FindAsync(10);
            Assert.NotNull(savedEvent);
            Assert.Equal("New Marathon", savedEvent!.EventName);
            Assert.Equal(1, context.Events.Count());
        }

        
        // PUT: api/events/{id}
     
        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            await SeedEventAsync(context, 1);
            var controller = new EventsController(context);

            var mismatchedEvent = new Events
            {
                EventID = 99, 
                EventName = "Mismatch"
            };

            // Act
            var result = await controller.Update(1, mismatchedEvent);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("The ID does not match", badRequest.Value);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenEventDoesNotExist()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new EventsController(context);

            var updateEvent = new Events
            {
                EventID = 42,
                EventName = "Ghost Event"
            };

            // Act
            var result = await controller.Update(42, updateEvent);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Update_UpdatesEvent_WhenValid()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            await SeedEventAsync(context, 1);
            var controller = new EventsController(context);

            var updatedEvent = new Events
            {
                EventID = 1,
                EventName = "Updated Name",
                Description = "Updated Description",
                Date = "2026-01-01",
                Distance = "20km",
                EventType = "Cycling",
                OrganiserID = 2,
                CategoryID = 3,
                LocationID = 4
            };

            // Act
            var result = await controller.Update(1, updatedEvent);

            // Assert
            Assert.IsType<OkObjectResult>(result);

            var dbEvent = await context.Events.FindAsync(1);
            Assert.NotNull(dbEvent);
            Assert.Equal("Updated Name", dbEvent!.EventName);
            Assert.Equal("Updated Description", dbEvent.Description);
            Assert.Equal("2026-01-01", dbEvent.Date);
            Assert.Equal("20km", dbEvent.Distance);
            Assert.Equal("Cycling", dbEvent.EventType);
            Assert.Equal(2, dbEvent.OrganiserID);
            Assert.Equal(3, dbEvent.CategoryID);
            Assert.Equal(4, dbEvent.LocationID);
        }

        
        // DELETE: api/events/{id}
      
        [Fact]
        public async Task DeleteEvents_ReturnsNotFound_WhenEventDoesNotExist()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new EventsController(context);

            // Act
            var result = await controller.DeleteEvents(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteEvents_RemovesEvent_WhenEventExists()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            await SeedEventAsync(context, 1);
            var controller = new EventsController(context);

            // Act
            var result = await controller.DeleteEvents(1);

            // Assert
            Assert.IsType<OkResult>(result);
            Assert.Empty(context.Events);

            var deletedEvent = await context.Events.FindAsync(1);
            Assert.Null(deletedEvent);
        }

        [Fact]
        public async Task DeleteEvents_OnlyRemovesTargetedEvent()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            await SeedEventAsync(context, 1);
            await SeedEventAsync(context, 2);
            await SeedEventAsync(context, 3);
            var controller = new EventsController(context);

            // Act
            var result = await controller.DeleteEvents(2);

            // Assert
            Assert.IsType<OkResult>(result);
            Assert.Equal(2, context.Events.Count());
            Assert.NotNull(await context.Events.FindAsync(1));
            Assert.Null(await context.Events.FindAsync(2));
            Assert.NotNull(await context.Events.FindAsync(3));
        }
    }
}