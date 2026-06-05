using Flunt.Notifications;
using Flunt.Validations;

namespace LibraryApi1.Dtos
{
    public class UserLoginDto : Notifiable<Notification>
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";

        public void Validate()
        {
            var contract = new Contract<UserLoginDto>()
                .Requires()
                .IsEmail(Email, nameof(Email), "Invalid email format")
                .IsGreaterOrEqualsThan(Password.Length, 6, nameof(Password), "Password must be at least 6 characters");

            AddNotifications(contract);
        }
    }
}
