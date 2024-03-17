using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeminarHub.Extensions;
using SeminarHub.Models.FormModels;
using SeminarHub.Models.ViewModels;
using SeminarHub.Services.Interfaces;
using System.Globalization;

namespace SeminarHub.Controllers
{
    [Authorize]
    public class SeminarController : Controller
    {
        private readonly ISeminarService seminarService;

        public SeminarController(ISeminarService seminarService)
        {
            this.seminarService = seminarService;
        }
        public async Task<IActionResult> All()
        {
            IEnumerable<AllSeminarsViewModel> allSeminars = await seminarService.GetAllSeminarsAsync();
            return View(allSeminars);
        }
        public async Task<IActionResult> Joined()
        {
            string organizerId = User.GetId();

            IEnumerable<JoinedSeminarViewModel> joinedSeminars
                = await seminarService.GetAllJoinedSeminarsAsync(organizerId);
            return View(joinedSeminars);
        }
        public async Task<IActionResult> Add()
        {
            AddSeminarFormModel model = new AddSeminarFormModel();
            
            var categories
                = await seminarService.GetAllCategoriesAsync();

            model.Categories = categories;            

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var seminar = await seminarService.GetSeminarByIdAsync(id);

            if (seminar == null)
            {
                return BadRequest();
            }
            if (seminar.OrganizerId != User.GetId())
            {
                return Unauthorized();
            }

            EditSeminarFormModel model = 
                await seminarService.GetSeminarForEditByIdAsync(id);

            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            var detailedEvent = await 
                seminarService.GetDetailsSeminarViewModelByIdAsync(id);

            if (detailedEvent == null)
            {
                return BadRequest();
            }

            return View(detailedEvent);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var seminar = await seminarService.GetSeminarByIdAsync(id);

            if (seminar == null)
            {
                return BadRequest();
            }
            if (seminar.OrganizerId != User.GetId())
            {
                return Unauthorized();
            }

            var seminarForDelete 
                = await seminarService.GetSeminarForDeleteByIdAsync(id);

            return View(seminarForDelete);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditSeminarFormModel model)
        {
            var seminar = await seminarService.GetSeminarByIdAsync(id);

            if (seminar == null)
            {
                return BadRequest();
            }
            if (seminar.OrganizerId != User.GetId())
            {
                return Unauthorized();
            }

            var categories
                = await seminarService.GetAllCategoriesAsync();

            model.Categories = categories;

            if (!DateTime.TryParseExact(model.DateAndTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                ModelState.AddModelError(nameof(model.DateAndTime), "Invalid date format. Please use format: dd/MM/yyyy HH:mm");
                return View(model);
            }


            await seminarService.EditSeminarAsync(id, model);
            return RedirectToAction("All", "Seminar");
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddSeminarFormModel model)
        {
            var categories
                = await seminarService.GetAllCategoriesAsync();

            model.Categories = categories;

            if (!DateTime.TryParseExact(model.DateAndTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                ModelState.AddModelError(nameof(model.DateAndTime), "Invalid date format. Please use format: dd/MM/yyyy HH:mm");
                return View(model);
            }

            string organizerId = User.GetId();

            await seminarService.AddSeminarAsync(model, organizerId);

            return RedirectToAction("All", "Seminar");
        }
        [HttpPost]
        public async Task<IActionResult>Join(int id)
        {
            var seminar = await seminarService.GetSeminarByIdAsync(id);

            if (seminar.OrganizerId == User.GetId())
            {
                return BadRequest();
            }

            string organizerId = User.GetId();

            JoinedSeminarViewModel joinedSeminar = await seminarService.GetJoinedSeminarByIdAsync(id);
            bool isJoined = await seminarService.IsTheSeminarJoinedAsync(id, organizerId);

            if (!isJoined)
            {
                if (joinedSeminar == null)
                {
                    return RedirectToAction("Joined", "Seminar");
                }

                await seminarService.JoinSeminarAsync(organizerId, joinedSeminar);

                return RedirectToAction("Joined", "Seminar");
            }

            return RedirectToAction("All", "Seminar");
        }
        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            var seminar = await seminarService.GetSeminarByIdAsync(id);

            if (seminar == null)
            {
                return BadRequest();
            }

            string organizerId = User.GetId();

            var leftSeminar = await seminarService.GetJoinedSeminarByIdAsync(id);

            if (leftSeminar == null)
            {
                return RedirectToAction("All", "Seminar");
            }

            await seminarService.LeaveSeminarAsync(organizerId, leftSeminar);
            return RedirectToAction("Joined", "Seminar");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seminar = await seminarService.GetSeminarByIdAsync(id);

            if (seminar == null)
            {
                return BadRequest();
            }
            if (seminar.OrganizerId != User.GetId())
            {
                return Unauthorized();
            }

            string organizerId = User.GetId();

            await seminarService.DeleteSeminarByIdAsync(id, organizerId);

            return RedirectToAction("All", "Seminar");
        }
    }
}
