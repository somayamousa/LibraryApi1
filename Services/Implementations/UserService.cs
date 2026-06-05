using LibraryApi1.Dtos;
using LibraryApi1.Models;
using LibraryApi1.Repositories;
using Microsoft.AspNetCore.Identity;

namespace LibraryApi1.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IJwtService _jwt;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public UserService(
            IUserRepository repo,
            IJwtService jwt,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole<int>> roleManager)
        {
            _repo = repo;
            _jwt = jwt;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public Task<UserProfileDto> RegisterAdmin(UserRegisterDto dto) => Register(dto, "ADMIN");
        public Task<UserProfileDto> RegisterLibrarian(UserRegisterDto dto) => Register(dto, "LIBRARIAN");
        public Task<UserProfileDto> RegisterMember(UserRegisterDto dto) => Register(dto, "MEMBER");

        private async Task<UserProfileDto> Register(UserRegisterDto dto, string roleUpper)
        {
            dto.Validate();
            if (dto==null) throw new ArgumentException(string.Join(" | ", dto.Notifications.Select(n => n.Message)));

            var exists = await _userManager.FindByEmailAsync(dto.Email);
            if (exists != null) throw new Exception("Email already exists");

            if (!await _roleManager.RoleExistsAsync(roleUpper))
            {
                await _roleManager.CreateAsync(new IdentityRole<int>(roleUpper));
            }

            var user = new AppUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName
            };

            var createResult = await _userManager.CreateAsync(user, dto.Password);
            if (!createResult.Succeeded)
                throw new Exception(string.Join(" | ", createResult.Errors.Select(e => e.Description)));

            await _userManager.AddToRoleAsync(user, roleUpper);

            

            var roles = await _userManager.GetRolesAsync(user);

            return new UserProfileDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = roles.FirstOrDefault() ?? roleUpper
            };
        }

        public async Task<string?> Login(UserLoginDto dto)
        {
            dto.Validate();
            if (dto==null) throw new ArgumentException(string.Join(" | ", dto.Notifications.Select(n => n.Message)));

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return null;

            if (!await _userManager.CheckPasswordAsync(user, dto.Password)) return null;

            var roles = await _userManager.GetRolesAsync(user);
            return _jwt.GenerateToken(user, roles);
        }

        public async Task<UserProfileDto?> GetProfile(int id)
        {
            var user = await _repo.GetUserByIdAsync(id);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);
            return new UserProfileDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = roles.FirstOrDefault() ?? ""
            };
        }

        public async Task<bool> UpdateProfile(int id, UserUpdateDto dto)
        {
            dto.Validate();
            if (dto==null) throw new ArgumentException(string.Join(" | ", dto.Notifications.Select(n => n.Message)));

            var user = await _repo.GetUserByIdAsync(id);
            if (user == null) return false;

            user.FullName = dto.FullName;
            user.Email = dto.Email;
            user.UserName = dto.Email;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return false;

            await _repo.UpdateUserAsync(user);
            return true;
        }

        public async Task UploadProfileImage(int id, string imageUrl)
        {
            var user = await _repo.GetUserByIdAsync(id);
            if (user == null) throw new Exception("User not found");
            user.ProfileImageUrl = imageUrl;
            await _userManager.UpdateAsync(user);
            await _repo.UpdateUserAsync(user);
        }
    }
}
