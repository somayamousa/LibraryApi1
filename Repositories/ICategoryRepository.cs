using LibraryApi1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApi1.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category category);

        Task<Category?> UpdateAsync(Category category);

        Task<Category?> GetCategoryWithBooksAsync(int id);
        Task DeleteConfirmedAsync(Category category);
    }
}
