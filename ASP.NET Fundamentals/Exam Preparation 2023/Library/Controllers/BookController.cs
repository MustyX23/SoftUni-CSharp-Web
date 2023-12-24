using Library.Contracts;
using Library.Extensions;
using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }
        public async Task<IActionResult> All()
        {
            var allBooks = await bookService.GetAllBooksAsync(); 
            return View(allBooks);
        }
        public async Task<IActionResult> Mine()
        {
            string userId = User.GetUserId();
            var myBooks = await bookService.GetMyBooksAsync(userId);

            return View(myBooks);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCollection(int id)
        {
            var book = await bookService.GetBookByIdAsync(id);
            var userId = User.GetUserId();

            if (book == null)
            {
                return RedirectToAction("All", "Book");
            }

            await bookService.AddBookToCollectionAsync(userId, book);
            return RedirectToAction("All", "Book");
        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int id)
        {
            var book = await bookService.GetBookByIdAsync(id);
            var userId = User.GetUserId();

            if (book == null)
            {
                return RedirectToAction("All", "Mine");
            }

            await bookService.RemoveFromCollectionAsync(userId, book);
            return RedirectToAction("Mine", "Book");
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddBookViewModel model = await bookService.GetNewBookForAddAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("All", "Book");
            }

            await bookService.AddBookAsync(model);
            return RedirectToAction("All", "Book");
        }
    }
}
