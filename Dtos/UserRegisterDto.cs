using Flunt.Notifications;
using Flunt.Validations;

namespace LibraryApi1.Dtos
{
    public class UserRegisterDto : Notifiable<Notification>
    {
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";

        public void Validate()
        {
            var contract = new Contract<UserRegisterDto>()
                .Requires()
                .IsNotNullOrEmpty(FullName, nameof(FullName), "Full name is required")
                .IsGreaterOrEqualsThan(FullName.Length, 3, nameof(FullName), "Full name must be at least 3 characters")
                .IsEmail(Email, nameof(Email), "Invalid email format")
                .IsGreaterOrEqualsThan(Password.Length, 6, nameof(Password), "Password must be at least 6 characters");

            AddNotifications(contract);
        }
    }
}
