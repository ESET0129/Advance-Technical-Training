using LibraryManagementAPI.Data;
using LibraryManagementAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDbContext _context;
        public AuthorRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _context.Authors.Include(a => a.Books).ToListAsync();
        }

        public async Task<Author?> GetAuthorByIdAsync(int id)
        {
            return await _context.Authors.Include(a => a.Books)
                                         .FirstOrDefaultAsync(a => a.AuthorID == id);
        }

        public async Task<Author> AddAuthorAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author?> UpdateAuthorAsync(Author author)
        {
            var existingAuthor = await _context.Authors.FindAsync(author.AuthorID);
            if (existingAuthor == null)
                return null;

            existingAuthor.Name = author.Name;
            existingAuthor.Country = author.Country;

            await _context.SaveChangesAsync();
            return existingAuthor;
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                return false;

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<Author?> UpdateAuthorAsync(int Id, Author author)
        {
            throw new NotImplementedException();
        }

        Task IAuthorRepository.UpdateAuthorAsync(Author author)
        {
            throw new NotImplementedException();
        }
    }
}
