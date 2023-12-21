using TaskBoardApp.Web.ViewModels.Board;

namespace TaskBoardApp.Services.Interfaces
{
    public interface IBoardService
    {
        Task<IEnumerable<BoardAllViewModel>> AllAsync();

        Task<IEnumerable<BoardSelectViewModel>> AllForSelectAsync();

        Task<bool> ExistsByIdAsync(int id);
        
    }
}
