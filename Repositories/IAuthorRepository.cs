// Repositories/IAuthorRepository.cs
using LibraryApi1.Models;

namespace LibraryApi1.Repositories
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
        Task<Author?> GetByNameAsync(string name);
        Task AddAsync(Author author);
        Task<bool> UpdateAsync(Author author);
        Task<bool> DeleteAsync(Author author);
        Task<bool> HasBooksAsync(int authorId);
    }
}
