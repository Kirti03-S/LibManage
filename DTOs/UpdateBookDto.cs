namespace LibManage.DTOs.Book;

public class UpdateBookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string ISBN { get; set; } = "";
    public DateTime PublishedDate { get; set; }
    public int Stock { get; set; }
    public int GenreId { get; set; }
    public int AuthorId { get; set; }
}
