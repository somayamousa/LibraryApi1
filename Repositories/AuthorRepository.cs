// Repositories/AuthorRepository.cs
using LibraryApi1.Data;
using LibraryApi1.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi1.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Authors
                .Include(a => a.BookAuthors)
                    .ThenInclude(ba => ba.Book)
                        .ThenInclude(b => b.Category)
                .ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Authors
                .Include(a => a.BookAuthors)
                    .ThenInclude(ba => ba.Book)
                        .ThenInclude(b => b.Category)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Author author)
        {
            _context.Authors.Update(author);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Author author)
        {
            _context.Authors.Remove(author);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Author?> GetByNameAsync(string name)
        {
            return await _context.Authors
                .FirstOrDefaultAsync(a => a.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> HasBooksAsync(int authorId)
        {
            return await _context.BookAuthors.AnyAsync(ba => ba.AuthorId == authorId);
        }
    }
}
