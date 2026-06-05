using LibraryApi1.Models;

namespace LibraryApi1.Repositories
{
    public interface IUserRepository
    {
        Task AddUserAsync(AppUser user);
        Task<AppUser?> GetUserByEmailAsync(string email);
        Task<AppUser?> GetUserByIdAsync(int id);
        Task UpdateUserAsync(AppUser user);
    }
}
