namespace LibManage.ViewModels
{
    public class PagedBookCatalogViewModel
    {
        public List<BookCatalogViewModel> Books { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}
