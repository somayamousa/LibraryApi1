using LibraryApi1.Dtos;
using LibraryApi1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi1.Controllers
{
    [ApiController]
    [Route("api/author/[action]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _service;

        public AuthorsController(IAuthorService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Policy = "ReadOnlyRoles")]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _service.GetAllAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadOnlyRoles")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _service.GetByIdAsync(id);

            if (author == null)
                throw new NotFoundException("Author not found");

            return Ok(author);
        }

        [HttpPost]
        [Authorize(Policy = "ManageCategories")]
        public async Task<IActionResult> Create([FromBody] CreateAuthorDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "ManageCategories")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAuthorDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);

            if (updated == null)
                throw new NotFoundException("Author not found");

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "ManageCategories")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);

            if (!success)
                throw new NotFoundException("Author not found");

            return NoContent();
        }
    }
}
