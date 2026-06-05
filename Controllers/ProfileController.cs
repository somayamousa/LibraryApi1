using LibraryApi1.Dtos;
using LibraryApi1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryApi1.Controllers
{
    [ApiController]
    [Route("api/profile/[action]")]
    public class ProfileController : ControllerBase
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        private int GetUserId()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id == null)
                throw new UnauthorizedException("Invalid JWT: missing user id");

            return int.Parse(id);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var profile = await _userService.GetProfile(GetUserId());

            if (profile == null)
                throw new NotFoundException("Profile not found");

            return Ok(profile);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UserUpdateDto dto)
        {
            bool success = await _userService.UpdateProfile(GetUserId(), dto);

            if (!success)
                throw new BadRequestException("Failed to update profile");

            return Ok(new { message = "Profile updated successfully" });
        }

        [HttpPost("profile-image")]
        [Authorize]
        public async Task<IActionResult> UploadProfileImage([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new BadRequestException("No file uploaded");

            var folder = Path.Combine("wwwroot", "profile-images");
            Directory.CreateDirectory(folder);

            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            string imageUrl = $"/profile-images/{fileName}";

            await _userService.UploadProfileImage(GetUserId(), imageUrl);

            return Ok(new { imageUrl });
        }
    }
}
