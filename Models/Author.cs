using System.Text.Json.Serialization;

namespace LibraryApi1.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
[JsonIgnore]  

        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}
