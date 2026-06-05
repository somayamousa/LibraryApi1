using LibraryApi1.Dtos;
using LibraryApi1.Models;
using LibraryApi1.Repositories;

namespace LibraryApi1.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IAuthorRepository _authorRepo;

        public BookService(
            IBookRepository bookRepo,
            ICategoryRepository categoryRepo,
            IAuthorRepository authorRepo)
        {
            _bookRepo = bookRepo;
            _categoryRepo = categoryRepo;
            _authorRepo = authorRepo;
        }

        public async Task<BookDetailsDto> CreateBookAsync(BookCreateDto dto)
        {
            dto.Validate();
            if (!dto.IsValid)
                throw new BadRequestException("Invalid book data");

            var category = await _categoryRepo.GetByIdAsync(dto.CategoryId)
                ?? throw new NotFoundException("Category not found");

            var book = new Book
            {
                Title = dto.Title,
                Isbn = dto.Isbn,
                CategoryId = dto.CategoryId,
                Quantity = dto.Quantity,
                PublicationYear = dto.PublicationYear,
                Description = dto.Description,
            };

            await _bookRepo.CreateAsync(book);

            foreach (var name in dto.AuthorNames)
            {
                var existing = await _authorRepo.GetByNameAsync(name.Trim());
                var author = existing ?? new Author { Name = name.Trim() };

                if (existing == null)
                    await _authorRepo.AddAsync(author);

                book.BookAuthors.Add(new BookAuthor
                {
                    BookId = book.Id,
                    AuthorId = author.Id
                });
            }

            await _bookRepo.UpdateAsync(book);

            return ToDetailsDto(book);
        }

        public async Task<IEnumerable<BookListDto>> GetAllBooksAsync()
        {
            var books = await _bookRepo.GetAllAsync();

            return books.Select(b => new BookListDto
            {
                Id = b.Id,
                Title = b.Title,
                Isbn = b.Isbn,
                PublicationYear = b.PublicationYear,
                Quantity = b.Quantity
            });
        }

        public async Task<BookDetailsDto> GetBookByIdAsync(int id)
        {
            var book = await _bookRepo.GetByIdAsync(id)
                ?? throw new NotFoundException("Book not found");

            return ToDetailsDto(book);
        }

        public async Task<BookDetailsDto> UpdateBookAsync(int id, BookUpdateDto dto)
        {
            dto.Validate();
            if (!dto.IsValid)
                throw new BadRequestException("Invalid book data");

            var book = await _bookRepo.GetByIdAsync(id)
                ?? throw new NotFoundException("Book not found");

            var category = await _categoryRepo.GetByIdAsync(dto.CategoryId)
                ?? throw new NotFoundException("Category not found");

            book.Title = dto.Title;
            book.Isbn = dto.Isbn;
            book.CategoryId = dto.CategoryId;
            book.Quantity = dto.Quantity;
            book.PublicationYear = dto.PublicationYear;
            book.Description = dto.Description;

            book.BookAuthors.Clear();

            foreach (var name in dto.AuthorNames)
            {
                var existing = await _authorRepo.GetByNameAsync(name.Trim());
                var author = existing ?? new Author { Name = name.Trim() };

                if (existing == null)
                    await _authorRepo.AddAsync(author);

                book.BookAuthors.Add(new BookAuthor
                {
                    BookId = book.Id,
                    AuthorId = author.Id
                });
            }

            await _bookRepo.UpdateAsync(book);

            return ToDetailsDto(book);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _bookRepo.GetByIdAsync(id)
                ?? throw new NotFoundException("Book not found");

            return await _bookRepo.DeleteAsync(book);
        }

        private BookDetailsDto ToDetailsDto(Book b)
        {
            return new BookDetailsDto
            {
                Id = b.Id,
                Title = b.Title,
                Isbn = b.Isbn,
                PublicationYear = b.PublicationYear,
                Quantity = b.Quantity,
                Description = b.Description,
                Category = b.Category?.Name ?? "",
                Authors = b.BookAuthors.Select(ba => ba.Author.Name).ToList()
            };
        }
    }
}
