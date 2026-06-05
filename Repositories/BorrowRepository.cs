using LibraryApi1.Data;
using LibraryApi1.Dtos;
using LibraryApi1.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi1.Repositories
{
    public class BorrowRepository : IBorrowRepository
    {
        private readonly AppDbContext _context;

        public BorrowRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Borrow> CreateAsync(Borrow borrow)
        {
            _context.Borrows.Add(borrow);
            await _context.SaveChangesAsync();
            return borrow;
        }

        public async Task<List<Borrow>> GetAllAsync()
        {
            return await _context.Borrows
                .Include(b => b.Book)
                .Include(b => b.User)
                .ToListAsync();
        }

        public async Task<Borrow?> GetByIdAsync(int id)
        {
            return await _context.Borrows
                .Include(b => b.Book)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<BorrowDto>> GetByUserDtoAsync(int userId)
        {
            return await _context.Borrows
                .Where(b => b.UserId == userId)
                .Select(b => new BorrowDto
                {
                    Id = b.Id,
                    BookId = b.BookId,
                    BookTitle = b.Book!.Title,
                    BorrowedAt = b.BorrowedAt,
                    DueAt = b.DueAt,
                    ReturnedAt = b.ReturnedAt,
                    Status = b.Status
                })
                .ToListAsync();
        }

        public async Task<Borrow> UpdateAsync(Borrow borrow)
        {
            _context.Borrows.Update(borrow);
            await _context.SaveChangesAsync();
            return borrow;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Borrows.FindAsync(id);
            if (entity == null) return false;

            _context.Borrows.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
