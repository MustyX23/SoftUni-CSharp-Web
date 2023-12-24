using Library.Contracts;
using Library.Data;
using Library.Data.Models;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private LibraryDbContext dbContext;

        public BookService(LibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddBookAsync(AddBookViewModel model)
        {
            Book book = new Book() 
            {
                Title = model.Title,
                Author = model.Author,
                CategoryId = model.CategoryId,
                ImageUrl = model.Url,
                Description = model.Description,
                Rating = model.Rating
            };

            dbContext.Books.Add(book);
            await dbContext.SaveChangesAsync();
        }

        public async Task AddBookToCollectionAsync(string userId, BookViewModel book)
        {
            bool bookAlreadyAdded = await dbContext.UserBooks.AnyAsync
                (ub => ub.CollectorId == userId && ub.Book.Id == book.Id);

            if (!bookAlreadyAdded)
            {
                var userBook = new IdentityUserBook()
                {
                    CollectorId = userId,
                    BookId = book.Id
                };

                await dbContext.UserBooks.AddAsync(userBook);
                await dbContext.SaveChangesAsync();
            }           
        }

        public async Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync()
        {
            IEnumerable<AllBookViewModel> allBooks = await dbContext.Books
                .Select(b => new AllBookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    ImageUrl = b.ImageUrl,
                    Rating = b.Rating,
                    Category = b.Category.Name
                })
                .ToArrayAsync();

            return allBooks;
        }

        public async Task<BookViewModel> GetBookByIdAsync(int id)
        {
            BookViewModel book = await dbContext
                .Books
                .Select (b => new BookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    ImageUrl = b.ImageUrl,
                    Rating = b.Rating,
                    Description = b.Description,
                    CategoryId = b.CategoryId
                }).FirstAsync(b => b.Id == id);

            return book;
        }

        public async Task<IEnumerable<MineBooksViewModel>> GetMyBooksAsync(string userId)
        {
            IEnumerable<MineBooksViewModel> myBooks = await dbContext
                .UserBooks
                .Where(b => b.CollectorId == userId)
                .Select(b => new MineBooksViewModel()
                {
                    Id = b.Book.Id,
                    Title = b.Book.Title,
                    Author = b.Book.Author,
                    ImageUrl = b.Book.ImageUrl,
                    Category = b.Book.Category.Name,
                    Description = b.Book.Description
                }).ToArrayAsync();

            return myBooks;
        }

        public async Task<AddBookViewModel> GetNewBookForAddAsync()
        {
            var categories = await dbContext.Categories
                .Select(c => new CategoryViewModel()
                {
                    Id= c.Id,
                    Name = c.Name
                }).ToArrayAsync();

            var book = new AddBookViewModel() 
            {
                Categories = categories
            };

            return book;
        }

        public async Task RemoveFromCollectionAsync(string userId, BookViewModel book)
        {
            var userBook = await dbContext.UserBooks
                    .FirstAsync(ub => ub.CollectorId == userId && ub.BookId == book.Id);

            if (userBook != null)
            {               
                dbContext.UserBooks.Remove(userBook);
                await dbContext.SaveChangesAsync();
            }

        }
    }
}
