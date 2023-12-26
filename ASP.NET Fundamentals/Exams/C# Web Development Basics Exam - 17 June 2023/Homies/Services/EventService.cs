using Homies.Data;
using Homies.Data.Models;
using Homies.Models;
using Homies.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Homies.Services
{
    
    public class EventService : IEventService
    {
        private readonly HomiesDbContext dbContext;

        public EventService(HomiesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddEventAsync(AddEventViewModel model, string ownerId)
        {
            Event currentEvent = new Event() 
            {
                Name = model.Name,
                Description = model.Description,
                Start = DateTime.Parse(model.Start),
                End = DateTime.Parse(model.End),
                TypeId = model.TypeId,
                CreatedOn = DateTime.Parse(model.CreatedOn),
                OrganiserId = ownerId
            };


            dbContext.Events.Add(currentEvent);

            await dbContext.SaveChangesAsync();
        }

        public async Task EditEventAsync(int id, AddEventViewModel model)
        {            
            Event eventForEdit = await dbContext.Events.FirstAsync(x => x.Id == id);

            eventForEdit.Name = model.Name;
            eventForEdit.Description = model.Description;
            eventForEdit.Start = DateTime.Parse(model.Start);
            eventForEdit.End = DateTime.Parse(model.End);
            eventForEdit.TypeId = model.TypeId;

            await dbContext.SaveChangesAsync();
        }

        public async Task<AddEventViewModel> GetAddEventViewModelAsync()
        {
            var types = await dbContext.Types.Select(t => new TypesViewModel()
            {
                Id= t.Id,
                Name= t.Name
            }).ToListAsync();

            var eventModel = new AddEventViewModel()
            {
                Types = types
            };

            return eventModel;
        }

        public async Task<AddEventViewModel> GetAddEventViewModelByIdAsync(int id)
        {
            var currentEvent = await dbContext.Events.FirstAsync(e => e.Id == id);
            var types = await dbContext.Types.Select(t => new TypesViewModel()
            {
                Id = t.Id,
                Name = t.Name
            }).ToListAsync();


            return new AddEventViewModel()
            {
                Name = currentEvent.Name,
                Description = currentEvent.Description,
                Start = currentEvent.Start.ToString("yyyy-MM-dd H:mm"),
                End = currentEvent.End.ToString("yyyy-MM-dd H:mm"),
                TypeId = currentEvent.TypeId,
                Types = types
            };
        }

        public async Task<IEnumerable<AllEventViewModel>> GetAllEventViewModelsAsync()
        {
            var allEvents = await dbContext.Events.Select(e => new AllEventViewModel
            {
                Id = e.Id,
                Name = e.Name,
                Start = e.Start.ToString("yyyy-MM-dd H:mm"),
                Type = e.Type.Name,
                Organiser = e.Organiser.Email,
            }).ToArrayAsync();

            return allEvents;
        }

        public async Task<IEnumerable<JoinedEventViewModel>> GetAllJoinedEventsViewModelsAsync(string userId)
        {
            var allEvents = await dbContext
                .EventParticipants
                .Where(e => e.HelperId == userId)
                .Select(e => new JoinedEventViewModel
            {
                Id = e.Event.Id,
                Name = e.Event.Name,
                Start = e.Event.Start.ToString("yyyy-MM-dd H:mm"),
                Type = e.Event.Type.Name,
                Organiser = e.Event.OrganiserId

            }).ToArrayAsync();

            return allEvents;
        }

        public async Task<DetailsEventViewModel> GetDetailsEventViewModelByIdAsync(int id)
        {

            var detailedEvent = await dbContext.Events
                .Where(e => e.Id == id)
                .Select(e => new DetailsEventViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    CreatedOn = e.CreatedOn.ToString("yyyy-MM-dd H:mm"),
                    Start = e.Start.ToString("yyyy-MM-dd H:mm"),
                    End = e.End.ToString("yyyy-MM-dd H:mm"),
                    Organiser = e.Organiser.Email,
                    Type = e.Type.Name
                }).FirstAsync();

            return detailedEvent;
        }

        public async Task<JoinedEventViewModel> GetJoinedEventByIdAsync(int id)
        {
            var joinedEvent = await dbContext.Events.FirstAsync(e => e.Id == id);

            var types = await dbContext.Types.Select(t => new TypesViewModel()
            {
                Id = t.Id,
                Name = t.Name
            }).ToListAsync();

            return new JoinedEventViewModel()
            {
                Id = joinedEvent.Id,
                Name= joinedEvent.Name,
                Organiser = joinedEvent.OrganiserId,
                Start = joinedEvent.Start.ToString("yyyy-MM-dd H:mm"),
                Types = types
            };
        }

        public async Task<bool> IsTheEventJoinedAsync(int id, string ownerId)
        {
            bool isJoined = await dbContext.EventParticipants
                .AnyAsync(e => e.EventId == id && e.HelperId == ownerId);

            return isJoined;
        }

        public async Task JoinEventAsync(string ownerId, JoinedEventViewModel joinedEvent)
        {
            var existingEvent = await dbContext
                .EventParticipants
                .FirstOrDefaultAsync(e => e.EventId == joinedEvent.Id && e.HelperId == ownerId);

            if (existingEvent == null)
            {
                var eventParticipant = new EventParticipant()
                {
                    EventId = joinedEvent.Id,
                    HelperId = ownerId,
                };

                dbContext.EventParticipants.Add(eventParticipant);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task LeaveEventAsync(string ownerId, JoinedEventViewModel joinedEvent)
        {
            var eventForRemoval = await dbContext
                .EventParticipants
                .FirstOrDefaultAsync(e => e.EventId == joinedEvent.Id && e.HelperId == ownerId);

            if (eventForRemoval != null)
            {
                dbContext.EventParticipants.Remove(eventForRemoval);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
