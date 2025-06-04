namespace LibManage.DTOs.Book
{

    
        public class BookDto
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string ISBN { get; set; }
            public DateTime PublishedDate { get; set; }
            public int Stock { get; set; }
            public int GenreId { get; set; }
            public int AuthorId { get; set; }
            public string GenreName { get; set; }
            public string AuthorName { get; set; }
        }

}

