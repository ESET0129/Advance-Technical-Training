using LibraryManagementAPI.Model;

namespace LibraryManagementAPI.Repository
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author?> GetAuthorByIdAsync(int Id);
        Task<Author> AddAuthorAsync(Author author);
        Task<Author?> UpdateAuthorAsync(int Id, Author author);
        Task<bool> DeleteAuthorAsync(int Id);
        Task UpdateAuthorAsync(Author author);
    }
}
