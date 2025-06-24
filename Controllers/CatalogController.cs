using LibManage.Controllers;
using LibManage.Models.LibManage.Models;
using LibManage.Services.Interfaces;
using LibManage.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LibManage.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class CatalogController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ILendingService _lendingService;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IBookService bookService, ILendingService lendingService, ILogger<CatalogController> logger)
        {
            _bookService = bookService;
            _lendingService = lendingService;
            _logger = logger;
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
        [HttpPost]
        public async Task<IActionResult> BorrowBook(int id)
        {
            var username = User.Identity?.Name;
            var message = await _lendingService.BorrowBookAsync(username!, id);

            if (message.StartsWith("Book successfully"))
                TempData["Success"] = message;
            else
                TempData["Error"] = message;

            return RedirectToAction(nameof(Index));
        }

    }
}



