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

        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();

            var viewModel = books.Select(b => new BookCatalogViewModel
            {
                Id = b.Id,
                Title = b.Title,
                ISBN = b.ISBN,
                AuthorName = b.AuthorName,
                GenreName = b.GenreName,
                Price = b.Price,
                CoverImageUrl = b.CoverImageUrl,
                Stock=b.Stock 
            }).ToList();

            return View(viewModel);
        }
    }
}
