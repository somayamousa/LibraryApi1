using LibraryApi1.Dtos;
using LibraryApi1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryApi1.Controllers
{
    [Route("api/user/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        private int GetUserId()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id == null)
                throw new UnauthorizedException("Invalid JWT: missing user id");

            return int.Parse(id);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> RegisterAdmin([FromBody] UserRegisterDto dto)
        {
            return Ok(await _service.RegisterAdmin(dto));
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> RegisterLibrarian([FromBody] UserRegisterDto dto)
        {
            return Ok(await _service.RegisterLibrarian(dto));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterMember([FromBody] UserRegisterDto dto)
        {
            return Ok(await _service.RegisterMember(dto));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var token = await _service.Login(dto);

            if (token == null)
                throw new UnauthorizedException("Invalid email or password");

            return Ok(new { token });
        }
    }
}
