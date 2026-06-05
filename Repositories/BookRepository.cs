using LibraryApi1.Data;
using LibraryApi1.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi1.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Book> CreateAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Category)
                .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book?> GetByIsbnAsync(string isbn)
        {
            return await _context.Books
                .Include(b => b.Category)
                .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                .FirstOrDefaultAsync(b => b.Isbn == isbn);
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books
                .Include(b => b.Category)
                .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Book book)
        {
            _context.Books.Remove(book);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
