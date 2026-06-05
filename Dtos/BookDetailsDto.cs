namespace LibraryApi1.Dtos
{
    public class BookDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Isbn { get; set; } = "";
        public int PublicationYear { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public string Category { get; set; } = "";
        public List<string> Authors { get; set; } = new();
    }
}
