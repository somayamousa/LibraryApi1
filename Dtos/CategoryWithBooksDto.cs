namespace LibraryApi1.Dtos
{
    public class CategoryWithBooksDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<BookDto> Books { get; set; } = new();
    }
}
