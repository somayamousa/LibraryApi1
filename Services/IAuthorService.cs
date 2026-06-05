// Services/IAuthorService.cs
using LibraryApi1.Dtos;

namespace LibraryApi1.Services
{
    public interface IAuthorService
    {
        Task<List<AuthorDto>> GetAllAsync();
        Task<AuthorWithBooksDto?> GetByIdAsync(int id);
        Task<AuthorDto> CreateAsync(CreateAuthorDto dto);
        Task<AuthorDto?> UpdateAsync(int id, UpdateAuthorDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
