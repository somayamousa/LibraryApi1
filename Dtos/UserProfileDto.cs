namespace LibraryApi1.Dtos
{
    public class UserProfileDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Role { get; set; } = "";
        public string? ProfileImageUrl { get; set; }
    }
}
