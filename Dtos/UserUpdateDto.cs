using Flunt.Notifications;
using Flunt.Validations;

namespace LibraryApi1.Dtos
{
    public class UserUpdateDto : Notifiable<Notification>
    {
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";

        public void Validate()
        {
            var contract = new Contract<UserUpdateDto>()
                .Requires()
                .IsNotNullOrEmpty(FullName, nameof(FullName), "Full name is required")
                .IsGreaterOrEqualsThan(FullName.Length, 3, nameof(FullName), "Full name must be at least 3 characters")
                .IsEmail(Email, nameof(Email), "Invalid email format");

            AddNotifications(contract);
        }
    }
}
