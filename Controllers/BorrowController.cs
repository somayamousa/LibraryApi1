using LibraryApi1.Dtos;
using LibraryApi1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi1.Controllers
{
    [ApiController]
    [Route("api/borrows/[action]")]
    public class BorrowsController : ControllerBase
    {
        private readonly IBorrowService _service;

        public BorrowsController(IBorrowService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = "MEMBER")]
        public async Task<IActionResult> CreateBorrow([FromBody] CreateBorrowDto dto)
        {
            var borrow = await _service.CreateAsync(dto);

            if (borrow == null)
                throw new BadRequestException("Borrow failed. Book unavailable or user not found.");

            return Ok(borrow);
        }

        [HttpGet]
        [Authorize(Policy = "ManageCategories")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }
        [HttpGet("{id:int}")]
        [Authorize(Policy = "ManageCategories")]
        public async Task<IActionResult> GetById(int id)
        {
            var record = await _service.GetByIdAsync(id);

            if (record == null)
                throw new NotFoundException("Borrow record not found");

            return Ok(record);
        }

        [HttpGet("{userId:int}")]
        [Authorize(Policy = "ManageCategories")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var records = await _service.GetByUserAsync(userId);
            return Ok(records);
        }

        [HttpPatch("{id:int}")]
        [Authorize(Policy = "ManageCategories")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateBorrowStatusDto dto)
        {
            var ok = await _service.ReturnAsync(id);

            if (ok == null)
                throw new NotFoundException("Borrow record not found");

            return Ok(new { message = "Status updated successfully" });
        }
        [HttpDelete("{id:int}")]
        [Authorize(Policy = "ManageCategories")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);

            if (!ok)
                throw new NotFoundException("Borrow record not found");

            return Ok(new { message = "Borrow deleted successfully" });
        }
    }
}
