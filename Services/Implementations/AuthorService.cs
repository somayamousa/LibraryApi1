// Services/Implementations/AuthorService.cs
using LibraryApi1.Dtos;
using LibraryApi1.Models;
using LibraryApi1.Repositories;

namespace LibraryApi1.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repo;

        public AuthorService(IAuthorRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<AuthorDto>> GetAllAsync()
        {
            var authors = await _repo.GetAllAsync();

            return authors.Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name
            }).ToList();
        }

        public async Task<AuthorWithBooksDto?> GetByIdAsync(int id)
        {
            var a = await _repo.GetByIdAsync(id);
            if (a == null) return null;

            return new AuthorWithBooksDto
            {
                Id = a.Id,
                Name = a.Name,
                Books = a.BookAuthors?
                         .Select(ba => new BookDto
                         {
                             Id = ba.Book?.Id ?? 0,
                             Title = ba.Book?.Title ?? string.Empty,
                             Isbn = ba.Book?.Isbn ?? string.Empty,
                             Category = ba.Book?.Category?.Name ?? string.Empty,
                             PublicationYear = ba.Book?.PublicationYear ?? 0,
                             Description = ba.Book?.Description ?? string.Empty,
                             Authors = ba.Book?.BookAuthors?
                                        .Select(x => x.Author?.Name ?? string.Empty)
                                        .Where(s => !string.IsNullOrEmpty(s))
                                        .ToList() ?? new List<string>()
                         })
                         .ToList() ?? new List<BookDto>()
            };
        }

        public async Task<AuthorDto> CreateAsync(CreateAuthorDto dto)
        {
            dto.Validate();
            if (!dto.IsValid)
                throw new BadRequestException("Invalid author data");

            var name = dto.Name.Trim();
            var exists = await _repo.GetByNameAsync(name);
            if (exists != null)
                throw new BadRequestException("Author with this name already exists");

            var author = new Author { Name = name };
            await _repo.AddAsync(author);

            return new AuthorDto { Id = author.Id, Name = author.Name };
        }

        public async Task<AuthorDto?> UpdateAsync(int id, UpdateAuthorDto dto)
        {
            dto.Validate();
            if (!dto.IsValid)
                throw new BadRequestException("Invalid author data");

            var author = await _repo.GetByIdAsync(id);
            if (author == null) return null;

            var name = dto.Name.Trim();
            var exists = await _repo.GetByNameAsync(name);
            if (exists != null && exists.Id != id)
                throw new BadRequestException("Another author with the same name exists");

            author.Name = name;

            var ok = await _repo.UpdateAsync(author);
            if (!ok) throw new Exception("Failed to update author");

            return new AuthorDto { Id = author.Id, Name = author.Name };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var author = await _repo.GetByIdAsync(id);
            if (author == null) return false;

            if (await _repo.HasBooksAsync(id))
                throw new BadRequestException("Cannot delete author with existing books");

            return await _repo.DeleteAsync(author);
        }
    }
}
