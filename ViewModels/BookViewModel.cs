using LibManage.Models;

namespace LibManage.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public int Stock { get; set; }

        public int? GenreId { get; set; } 
        public Genre? Genre { get; set; }

        public int? AuthorId { get; set; }
        public Author? Author { get; set; }
    }

}

