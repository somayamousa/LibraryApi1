using Flunt.Notifications;
using Flunt.Validations;

namespace LibraryApi1.Dtos
{
    public class CreateBorrowDto : Notifiable<Notification>
    {
        public int UserId { get; set; }
        public int BookId { get; set; }

        public void Validate()
        {
            var contract = new Contract<CreateBorrowDto>()
                .Requires()
                .IsGreaterThan(UserId, 0, "UserId", "UserId must be greater than 0")
                .IsGreaterThan(BookId, 0, "BookId", "BookId must be greater than 0");

            AddNotifications(contract);
        }
    }
}
