using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBoard.App.Extensions;
using TaskBoardApp.Services.Interfaces;
using TaskBoardApp.Web.ViewModels.Task;

namespace TaskBoard.App.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly IBoardService boardService;
        private readonly ITaskService taskService;

        public TaskController(IBoardService boardService, ITaskService taskService)
        {
            this.boardService = boardService;
            this.taskService = taskService;

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            TaskFormModel viewModel = new TaskFormModel() 
            {
                AllBoards = await boardService.AllForSelectAsync()
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TaskFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AllBoards = await boardService.AllForSelectAsync();
                return View(model);
            }
            bool boardExists = await this.boardService.ExistsByIdAsync(model.BoardId);

            if (!boardExists)
            {
                ModelState.AddModelError(nameof(model.BoardId), "Selected board doesn't exist.");
                model.AllBoards = await boardService.AllForSelectAsync();
                return View(model);
            }

            string currentUserId = this.User.GetId();

            await taskService.AddAsync(currentUserId, model);

            return RedirectToAction("All", "Board");
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            TaskDetailsViewModel viewModel = await taskService.GetForDetailsByIdAsync(id);

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {

        }
    }
}
