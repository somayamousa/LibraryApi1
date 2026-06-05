using Flunt.Notifications;
using Flunt.Validations;

namespace LibraryApi1.Dtos
{
    public class BookUpdateDto : Notifiable<Notification>
    {
        public string Title { get; set; } = "";
        public string Isbn { get; set; } = "";
        public int CategoryId { get; set; }
        public int Quantity { get; set; }
        public int PublicationYear { get; set; }
        public string? Description { get; set; }
        public List<string> AuthorNames { get; set; } = new();

        public void Validate()
        {
            var contract = new Contract<BookUpdateDto>()
                .Requires()
                .IsNotNullOrEmpty(Title, "Title", "Title is required")
                .IsNotNullOrEmpty(Isbn, "Isbn", "ISBN is required")
                .IsGreaterThan(CategoryId, 0, "CategoryId", "CategoryId must be > 0")
                .IsGreaterThan(Quantity, 0, "Quantity", "Quantity must be > 0")
                .IsGreaterThan(PublicationYear, 1000, "PublicationYear", "Invalid year")
                .IsGreaterThan(AuthorNames.Count, 0, "AuthorNames", "At least one author required");

            AddNotifications(contract);
        }
    }
}
