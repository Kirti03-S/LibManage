using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace LibManage.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string ISBN { get; set; } = "";
    public DateTime PublishedDate { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; } 

    // Foreign Keys
    public int GenreId { get; set; }
    public Genre? Genre { get; set; }

    public int AuthorId { get; set; }
    public Author? Author { get; set; }

    // Navigation
    public ICollection<Review>? Reviews { get; set; }
    public ICollection<LendingRecord>? LendingRecords { get; set; }
    public string? CoverImageUrl { get; set; }

}

