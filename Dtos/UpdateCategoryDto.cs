using Flunt.Notifications;
using Flunt.Validations;

namespace LibraryApi1.Dtos.Categories
{
    public class UpdateCategoryDto : Notifiable<Notification>
    {
        public string Name { get; set; } = string.Empty;

        public void Validate()
        {
            var contract = new Contract<UpdateCategoryDto>()
                .Requires()
                .IsNotNullOrEmpty(Name, "Name", "Name is required")
                .IsGreaterOrEqualsThan(Name.Length, 3, "Name", "Name must be at least 3 characters");

            AddNotifications(contract);
        }
    }
}
