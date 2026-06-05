namespace LibraryApi1.Dtos
{
    public class BookListDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Isbn { get; set; } = "";
        public int PublicationYear { get; set; }
        public int Quantity { get; set; }
    }
}
