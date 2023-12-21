using System.ComponentModel.DataAnnotations;
using TaskBoardApp.Web.ViewModels.Board;
using static TaskboardApp.Common.EntityValidationConstants.Task;

namespace TaskBoardApp.Web.ViewModels.Task
{
    public class TaskFormModel
    {
        [Required]
        [StringLength(TaskMaxTitle, MinimumLength = TaskMinTitle
            , ErrorMessage = "Title should be atleast {2} characters long.")]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(TaskMaxDescription, MinimumLength = TaskMinDescription
            , ErrorMessage = "Description should be atleast {2} characters long.")]
        public string Description { get; set; } = null!;

        [Display(Name = "Board")]
        public int BoardId {  get; set; }

        public IEnumerable<BoardSelectViewModel>? AllBoards { get; set; }
    }
}
