using Library.Models;

namespace Library.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync();

        Task<IEnumerable<MineBooksViewModel>> GetMyBooksAsync(string userId);

        Task<BookViewModel> GetBookByIdAsync(int id);

        Task AddBookToCollectionAsync(string userId, BookViewModel book);

        Task RemoveFromCollectionAsync(string userId, BookViewModel book);

        Task AddBookAsync(AddBookViewModel model);
        Task<AddBookViewModel> GetNewBookForAddAsync();
    }
}
