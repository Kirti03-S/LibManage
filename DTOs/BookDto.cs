namespace LibManage.DTOs.Book
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int GenreId { get; set; }
        public string GenreName { get; set; } 

        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string? CoverImageUrl { get; set; }
    }
}
