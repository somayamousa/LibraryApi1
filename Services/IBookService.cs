using LibraryApi1.Dtos;

namespace LibraryApi1.Services
{
    public interface IBookService
    {
        Task<BookDetailsDto> CreateBookAsync(BookCreateDto dto);
        Task<IEnumerable<BookListDto>> GetAllBooksAsync();
        Task<BookDetailsDto> GetBookByIdAsync(int id);
        Task<BookDetailsDto> UpdateBookAsync(int id, BookUpdateDto dto);
        Task<bool> DeleteBookAsync(int id);
    }
}
