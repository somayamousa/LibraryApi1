using Flunt.Notifications;
using Flunt.Validations;

namespace LibraryApi1.Dtos.Categories
{
    public class CreateCategoryDto : Notifiable<Notification>
    {
        public string Name { get; set; } = string.Empty;

        public void Validate()
        {
            var contract = new Contract<CreateCategoryDto>()
                .Requires()
                .IsNotNullOrWhiteSpace(Name, "Name", "Category name is required")
                .IsGreaterOrEqualsThan(Name.Length, 3, "Name", "Category name must be at least 3 characters");

            AddNotifications(contract);
        }
    }
}
