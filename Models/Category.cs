using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryApi1.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        [JsonIgnore]
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
