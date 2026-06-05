using LibraryApi1.Dtos;
using LibraryApi1.Dtos.Categories;
using LibraryApi1.Services;
using LibraryApi1.Models;
using LibraryApi1.Repositories;

namespace LibraryApi1.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _repo.GetAllAsync();

            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            });
        }

        public async Task<CategoryWithBooksDto> GetCategoryByIdAsync(int id)
        {
            var c = await _repo.GetCategoryWithBooksAsync(id);
            if (c == null)
                throw new NotFoundException("Category not found");

            return new CategoryWithBooksDto
            {
                Id = c.Id,
                Name = c.Name,
                Books = c.Books.Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Isbn = b.Isbn,
                    PublicationYear = b.PublicationYear,
                    Description = b.Description,
                    Category = c.Name,
                    Authors = b.BookAuthors.Select(a => a.Author!.Name).ToList()
                }).ToList()
            };
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name
            };

            var created = await _repo.CreateAsync(category);

            return new CategoryDto
            {
                Id = created.Id,
                Name = created.Name
            };
        }

        public async Task<CategoryDto> UpdateCategoryAsync(int id, UpdateCategoryDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                throw new NotFoundException("Category not found");

            existing.Name = dto.Name;
            var updated = await _repo.UpdateAsync(existing);

            return new CategoryDto
            {
                Id = updated!.Id,
                Name = updated.Name
            };
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var c = await _repo.GetByIdAsync(id);
            if (c == null)
                throw new NotFoundException("Category not found");

            if (c.Books.Any())
                throw new BadRequestException("Cannot delete a category that contains books.");

            await _repo.DeleteConfirmedAsync(c);
            return true;
        }
    }
}
