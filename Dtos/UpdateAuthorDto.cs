// Dtos/UpdateAuthorDto.cs
using Flunt.Notifications;
using Flunt.Validations;

namespace LibraryApi1.Dtos
{
    public class UpdateAuthorDto : Notifiable<Notification>
    {
        public string Name { get; set; } = string.Empty;

        public void Validate()
        {
            var contract = new Contract<UpdateAuthorDto>()
                .Requires()
                .IsNotNullOrEmpty(Name, nameof(Name), "Author name is required")
                .IsGreaterOrEqualsThan(Name?.Trim().Length ?? 0, 3, nameof(Name), "Name must be at least 3 characters");

            AddNotifications(contract);
        }
    }
}
