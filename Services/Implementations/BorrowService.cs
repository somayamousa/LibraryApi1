using LibraryApi1.Dtos;
using LibraryApi1.Services;
using LibraryApi1.Models;
using LibraryApi1.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi1.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly IBorrowRepository _borrowRepo;
        private readonly IUserRepository _userRepo;
        private readonly IBookRepository _bookRepo;

        public BorrowService(
            IBorrowRepository borrowRepo,
            IUserRepository userRepo,
            IBookRepository bookRepo)
        {
            _borrowRepo = borrowRepo;
            _userRepo = userRepo;
            _bookRepo = bookRepo;
        }

       
        public async Task<BorrowDto> CreateAsync(CreateBorrowDto dto)
        {
            dto.Validate();
            if (!dto.IsValid)
                throw new BadRequestException("UserId must be greater than 0 orBookId must be greater than 0");

            var user = await _userRepo.GetUserByIdAsync(dto.UserId);
            if (user == null)
                throw new NotFoundException("User not found");

            var book = await _bookRepo.GetByIdAsync(dto.BookId);
            if (book == null)
                throw new NotFoundException("Book not found");

            var borrow = new Borrow
            {
                UserId = dto.UserId,
                BookId = dto.BookId,
                BorrowedAt = DateTime.UtcNow,
                DueAt = DateTime.UtcNow.AddDays(14),
                Status = "Borrowed"
            };

            await _borrowRepo.CreateAsync(borrow);

            return new BorrowDto
            {
                Id = borrow.Id,
                BookId = borrow.BookId,
                BookTitle = book.Title,
                BorrowedAt = borrow.BorrowedAt,
                DueAt = borrow.DueAt,
                ReturnedAt = borrow.ReturnedAt,
                Status = borrow.Status
            };
        }

  
        public async Task<List<BorrowDto>> GetAllAsync()
        {
            var data = await _borrowRepo.GetAllAsync();

            return data.Select(b => new BorrowDto
            {
                Id = b.Id,
                BookId = b.BookId,
                BookTitle = b.Book!.Title,
                BorrowedAt = b.BorrowedAt,
                DueAt = b.DueAt,
                ReturnedAt = b.ReturnedAt,
                Status = b.Status
            }).ToList();
        }

        public async Task<BorrowDto> GetByIdAsync(int id)
        {
            var b = await _borrowRepo.GetByIdAsync(id);
            if (b == null)
                throw new NotFoundException("Borrow record not found");

            return new BorrowDto
            {
                Id = b.Id,
                BookId = b.BookId,
                BookTitle = b.Book!.Title,
                BorrowedAt = b.BorrowedAt,
                DueAt = b.DueAt,
                ReturnedAt = b.ReturnedAt,
                Status = b.Status
            };
        }

        public async Task<List<BorrowDto>> GetByUserAsync(int userId)
        {
            return await _borrowRepo.GetByUserDtoAsync(userId);
        }

        public async Task<BorrowDto> ReturnAsync(int id)
        {
            var borrow = await _borrowRepo.GetByIdAsync(id);
            if (borrow == null)
                throw new NotFoundException("Borrow record not found");

            if (borrow.Status == "Returned")
                throw new BadRequestException("Book is already returned");

            borrow.Status = "Returned";
            borrow.ReturnedAt = DateTime.UtcNow;

            await _borrowRepo.UpdateAsync(borrow);

            return new BorrowDto
            {
                Id = borrow.Id,
                BookId = borrow.BookId,
                BookTitle = borrow.Book!.Title,
                BorrowedAt = borrow.BorrowedAt,
                DueAt = borrow.DueAt,
                ReturnedAt = borrow.ReturnedAt,
                Status = borrow.Status
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await _borrowRepo.DeleteAsync(id);
            if (!deleted)
                throw new NotFoundException("Borrow record not found");

            return true;
        }
    }
}
