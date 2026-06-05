using Microsoft.AspNetCore.Identity;

namespace LibraryApi1.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string FullName { get; set; } = "";

        public string? ProfileImageUrl { get; set; }

        public ICollection<Borrow> Borrows { get; set; } = new List<Borrow>();
    }
}
