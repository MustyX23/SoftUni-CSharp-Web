using Homies.Models;

namespace Homies.Services.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<AllEventViewModel>> GetAllEventViewModelsAsync();

        Task<AddEventViewModel> GetAddEventViewModelAsync();

        Task AddEventAsync(AddEventViewModel model, string ownerId);

        Task<IEnumerable<JoinedEventViewModel>> GetAllJoinedEventsViewModelsAsync(string ownerId);
        Task EditEventAsync(int id, AddEventViewModel model);
        Task<AddEventViewModel> GetAddEventViewModelByIdAsync(int id);
        Task<JoinedEventViewModel> GetJoinedEventByIdAsync(int id);
        Task JoinEventAsync(string ownerId, JoinedEventViewModel joinedEvent);

        Task LeaveEventAsync(string ownerId, JoinedEventViewModel joinedEvent);

        Task<DetailsEventViewModel> GetDetailsEventViewModelByIdAsync(int id);
        Task<bool> IsTheEventJoinedAsync(int id, string userId);
    }
}
