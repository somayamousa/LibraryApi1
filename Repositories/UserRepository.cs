using LibraryApi1.Data;
using LibraryApi1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi1.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;

        public UserRepository(AppDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task AddUserAsync(AppUser user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }

        public Task<AppUser?> GetUserByEmailAsync(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }

        public Task<AppUser?> GetUserByIdAsync(int id)
        {
            return _db.Users.FindAsync(id).AsTask();
        }

        public async Task UpdateUserAsync(AppUser user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }
    }
}
