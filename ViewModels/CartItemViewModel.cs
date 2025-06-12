namespace LibManage.ViewModels
{
    public class CartItemViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string GenreName { get; set; }
        public decimal Price { get; set; }
        public string CoverImageUrl { get; set; }
        public int Stock { get; set; }
        public decimal TotalPrice => Price * Stock;
    }
}

