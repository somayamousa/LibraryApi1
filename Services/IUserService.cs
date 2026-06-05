using LibraryApi1.Dtos;

namespace LibraryApi1.Services
{
    public interface IUserService
    {
        Task<UserProfileDto> RegisterAdmin(UserRegisterDto dto);
        Task<UserProfileDto> RegisterLibrarian(UserRegisterDto dto);
        Task<UserProfileDto> RegisterMember(UserRegisterDto dto);

        Task<string?> Login(UserLoginDto dto);

        Task<UserProfileDto?> GetProfile(int id);
        Task<bool> UpdateProfile(int id, UserUpdateDto dto);

        Task UploadProfileImage(int id, string imageUrl);
    }
}
