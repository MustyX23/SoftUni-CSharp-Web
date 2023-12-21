using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBoardApp.Services.Interfaces;
using TaskBoardApp.Web.ViewModels.Board;

namespace TaskBoard.App.Controllers
{
    [Authorize]
    public class BoardController : Controller
    {
        private readonly IBoardService boardService;

        public BoardController(IBoardService boardService)
        {
            this.boardService = boardService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            IEnumerable<BoardAllViewModel> allBoards = await boardService.AllAsync();
            return View(allBoards);
        }
    }
}
