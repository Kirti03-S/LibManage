namespace LibManage.ViewModels
{
    public class BookCatalogViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string AuthorName { get; set; }
        public string GenreName { get; set; }
        public decimal Price { get; set; }
        public string CoverImageUrl { get; set; }
    }
}
