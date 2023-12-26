using Homies.Extensions;
using Homies.Models;
using Homies.Services;
using Homies.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace Homies.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }
        public async Task<IActionResult> All() 
        {
            var allEvents = await eventService.GetAllEventViewModelsAsync();
            return View(allEvents);
        }

        public async Task<IActionResult> Add()
        {
            var model = await eventService.GetAddEventViewModelAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEventViewModel model)
        {
            string ownerId = User.GetUserId();

            await eventService.AddEventAsync(model, ownerId);

            return RedirectToAction("All", "Event");   
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            string ownerId = User.GetUserId();

            var currentEvent = await eventService.GetAddEventViewModelByIdAsync(id);

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("All", "Event");
            }

            return View(currentEvent);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddEventViewModel model)
        {
            string ownerId = User.GetUserId();

            await eventService.EditEventAsync(id, model);
            return RedirectToAction("All", "Event");
        }

        public async Task<IActionResult> Joined()
        {
            string ownerId = User.GetUserId();

            var joinedEvents = await eventService.GetAllJoinedEventsViewModelsAsync(ownerId);
            return View(joinedEvents);
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            string ownerId = User.GetUserId();

            var joinedEvent = await eventService.GetJoinedEventByIdAsync(id);
            bool isJoined = await eventService.IsTheEventJoinedAsync(id, ownerId);

            if (!isJoined)
            {
                if (joinedEvent == null)
                {
                    return RedirectToAction("Joined", "Event");
                }

                await eventService.JoinEventAsync(ownerId, joinedEvent);

                return RedirectToAction("Joined", "Event");
            }

            return RedirectToAction("All", "Event");
        }
        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            string ownerId = User.GetUserId();

            var leftEvent = await eventService.GetJoinedEventByIdAsync(id);

            if (leftEvent == null)
            {
                return RedirectToAction("All", "Event");
            }

            await eventService.LeaveEventAsync(ownerId, leftEvent);
            return RedirectToAction("All", "Event");
        }

        public async Task<IActionResult> Details(int id)
        {
            string ownerId = User.GetUserId();
            var detailedEvent = await eventService.GetDetailsEventViewModelByIdAsync(id);

            return View(detailedEvent);
        }
    }
}
