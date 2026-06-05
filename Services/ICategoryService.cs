using LibraryApi1.Dtos;
using LibraryApi1.Dtos.Categories;

namespace LibraryApi1.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryWithBooksDto> GetCategoryByIdAsync(int id);
        Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto);
        Task<CategoryDto> UpdateCategoryAsync(int id, UpdateCategoryDto dto);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
