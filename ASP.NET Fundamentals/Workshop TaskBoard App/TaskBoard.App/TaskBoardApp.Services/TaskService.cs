using Microsoft.EntityFrameworkCore;
using TaskBoard.App.Data;
using TaskBoardApp.Services.Interfaces;
using TaskBoardApp.Web.ViewModels.Task;

namespace TaskBoardApp.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskBoardDbContext _dbContext;

        public TaskService(TaskBoardDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(string ownerId, TaskFormModel viewModel)
        {
            Data.Models.Task task = new Data.Models.Task()
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                BoardId = viewModel.BoardId,
                CreatedOn = DateTime.UtcNow,
                OwnerId = ownerId
            };

            await this._dbContext.Tasks.AddAsync(task);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(string id)
        {
            //will check later...
        }

        public async Task<TaskDetailsViewModel> GetForDetailsByIdAsync(string id)
        {
            TaskDetailsViewModel viewModel = await _dbContext
                .Tasks
                .Select(t => new TaskDetailsViewModel()
                {
                    Id = t.Id.ToString(),
                    Title = t.Title,
                    Description = t.Description,
                    Board = t.Board.Name.ToString(),
                    CreatedOn = t.CreatedOn.ToString("f"),
                    Owner = t.Owner.UserName
                })
                .FirstAsync(t => t.Id == id);

            return viewModel;
        }
    }
}
