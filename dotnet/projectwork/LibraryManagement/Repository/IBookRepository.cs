using LibraryManagementAPI.Model;

namespace LibraryManagementAPI.Repository
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int Id);
        Task<Book> AddBookAsync(Book book);
        Task<Book?> UpdateBookAsync(int Id, Book book);
        Task<bool> DeleteBookAsync(int Id);
        Task UpdateBookAsync(Book book);
    }
}
