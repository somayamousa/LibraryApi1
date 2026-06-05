using Flunt.Notifications;
using Flunt.Validations;

namespace LibraryApi1.Dtos
{
    public class UpdateBorrowStatusDto : Notifiable<Notification>
    {
        public string Status { get; set; } = string.Empty;

        public void Validate()
        {
            var allowed = new[] { "Borrowed", "Returned", "Late" };

            var contract = new Contract<UpdateBorrowStatusDto>()
                .Requires()
                .IsNotNullOrEmpty(Status, "Status", "Status is required")
                .IsTrue(allowed.Contains(Status), "Status", "Invalid status value");

            AddNotifications(contract);
        }
    }
}
