namespace LibManage.DTOs.Book
{
    public class CreateBookDto
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        public string? CoverImageUrl { get; set; }
    }

}
