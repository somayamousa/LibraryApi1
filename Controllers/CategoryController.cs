using LibraryApi1.Dtos.Categories;
using LibraryApi1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi1.Controllers
{
    [ApiController]
    [Route("api/category/[action]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Policy = "ReadOnlyRoles")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllCategoriesAsync());
        }

        [HttpGet("{id:int}")]
        [Authorize(Policy = "ReadOnlyRoles")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var result = await _service.GetCategoryByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = "ManageCategories")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
        {
            dto.Validate();
            if (!dto.IsValid)
                throw new BadRequestException("Invalid category data");

            var created = await _service.CreateCategoryAsync(dto);
            return Ok(created);
        }

        [HttpPut("{id:int}")]
        [Authorize(Policy = "ManageCategories")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto dto)
        {
            dto.Validate();
            if (!dto.IsValid)
                throw new BadRequestException("Invalid category data");

            var updated = await _service.UpdateCategoryAsync(id, dto);
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = "ManageCategories")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteCategoryAsync(id);
            return Ok(new { message = "Category deleted successfully" });
        }
    }
}
