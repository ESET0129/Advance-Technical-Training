using LibraryManagementAPI.Data;
using LibraryManagementAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books.Include(b => b.Author).ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _context.Books.Include(b => b.Author)
                                       .FirstOrDefaultAsync(b => b.BookId == id);
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book?> UpdateBookAsync(Book book)
        {
            var existingBook = await _context.Books.FindAsync(book.BookId);
            if (existingBook == null)
                return null;

            existingBook.Title = book.Title;
            existingBook.Genre = book.Genre;
            existingBook.AuthorId = book.AuthorId;

            await _context.SaveChangesAsync();
            return existingBook;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<Book?> UpdateBookAsync(int Id, Book book)
        {
            throw new NotImplementedException();
        }

        Task IBookRepository.UpdateBookAsync(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
