namespace LibraryApi1.Dtos
{
    public class AuthorWithBooksDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<BookDto> Books { get; set; } = new();
    }
}
