using LibraryApi1.Dtos;
using LibraryApi1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi1.Controllers
{
    [ApiController]
    [Route("api/book/[action]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        [Authorize(Policy = "ManageCategories")]
        public async Task<IActionResult> Create([FromBody] BookCreateDto dto)
        {
            dto.Validate();
            if (!dto.IsValid)
                throw new BadRequestException("Invalid book data");

            var result = await _bookService.CreateBookAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }


        [HttpGet("{id:int}")]
        [Authorize(Policy = "ReadOnlyRoles")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
                throw new NotFoundException("Book not found");

            return Ok(book);
        }


        [HttpGet]
        [Authorize(Policy = "ReadOnlyRoles")]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }


        [HttpPut("{id:int}")]
        [Authorize(Policy = "ManageCategories")]
        public async Task<IActionResult> Update(int id, [FromBody] BookUpdateDto dto)
        {
            dto.Validate();
            if (!dto.IsValid)
                throw new BadRequestException("Invalid book data");

            var updated = await _bookService.UpdateBookAsync(id, dto);

            return Ok(updated);
        }


        // ============================================================
        // DELETE
        // ============================================================
        [HttpDelete("{id:int}")]
        [Authorize(Policy = "ManageCategories")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteBookAsync(id);

            return NoContent();
        }
    }
}
