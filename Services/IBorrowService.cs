using LibraryApi1.Dtos;
using LibraryApi1.Models;

namespace LibraryApi1.Services
{
    public interface IBorrowService
    {
        Task<BorrowDto> CreateAsync(CreateBorrowDto dto);       // MEMBER
        Task<List<BorrowDto>> GetAllAsync();                    // ADMIN, LIBRARIAN
        Task<BorrowDto> GetByIdAsync(int id);                   // ADMIN, LIBRARIAN
        Task<List<BorrowDto>> GetByUserAsync(int userId);       // ADMIN, LIBRARIAN
        Task<BorrowDto> ReturnAsync(int id);                    // ADMIN, LIBRARIAN  (بدون إدخال Status)
        Task<bool> DeleteAsync(int id);                         // ADMIN, LIBRARIAN
    }
}
