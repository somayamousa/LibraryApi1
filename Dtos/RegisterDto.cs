using Flunt.Notifications;
using Flunt.Validations;

namespace LibraryApi1.Dtos
{
    public class RegisterDto : Notifiable<Notification>
    {
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";

        public void Validate()
        {
            var contract = new Contract<RegisterDto>()
                .Requires()
                .IsNotNullOrEmpty(FullName, "FullName", "Full name is required")
                .IsGreaterOrEqualsThan(FullName.Length, 3, "FullName", "Full name must be at least 3 characters")

                .IsEmail(Email, "Email", "Invalid email format")

                .IsGreaterOrEqualsThan(Password.Length, 6, "Password", "Password must be at least 6 characters");

            AddNotifications(contract);
        }
    }
}
