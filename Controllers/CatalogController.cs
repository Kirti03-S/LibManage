using LibManage.Services.Interfaces;
using LibManage.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibManage.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class CatalogController : Controller
    {
        private readonly IBookService _bookService;

        public CatalogController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 6;
            var (books,totalCount) = await _bookService.GetPagedBooksAsync(page, pageSize);

            var viewModel = new PagedBookCatalogViewModel
            {
                Books = books.Select(b => new BookCatalogViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    ISBN = b.ISBN,
                    AuthorName = b.AuthorName,
                    GenreName = b.GenreName,
                    Price = b.Price,
                    CoverImageUrl = b.CoverImageUrl,
                    Stock = b.Stock
                }).ToList(),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };

            return View(viewModel);
        }

    }
}
