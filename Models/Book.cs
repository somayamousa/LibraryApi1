using System.Text.Json.Serialization;

namespace LibraryApi1.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Isbn { get; set; } = string.Empty;
        public int Quantity { get; set; }

        public int CategoryId { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }

        public int PublicationYear { get; set; }

        public string? Description { get; set; }

        public string? CoverImageUrl { get; set; }


        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();

        public ICollection<Borrow> Borrows { get; set; } = new List<Borrow>();

    }
}
