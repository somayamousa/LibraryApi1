using LibraryApi1.Dtos;
using LibraryApi1.Models;

namespace LibraryApi1.Repositories
{
    public interface IBorrowRepository
    {
        Task<Borrow> CreateAsync(Borrow borrow);
        Task<List<Borrow>> GetAllAsync();
        Task<Borrow?> GetByIdAsync(int id);
        Task<List<BorrowDto>> GetByUserDtoAsync(int userId);
        Task<Borrow> UpdateAsync(Borrow borrow);
        Task<bool> DeleteAsync(int id);
    }
}
