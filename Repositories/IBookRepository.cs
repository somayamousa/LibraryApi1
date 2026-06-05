using LibraryApi1.Models;

namespace LibraryApi1.Repositories
{
    public interface IBookRepository
    {
        Task<Book> CreateAsync(Book book);
        Task<Book?> GetByIdAsync(int id);
        Task<Book?> GetByIsbnAsync(string isbn);
        Task<List<Book>> GetAllAsync();
        Task<bool> UpdateAsync(Book book);
        Task<bool> DeleteAsync(Book book);
    }
}
