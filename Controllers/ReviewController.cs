using LibManage.DTOs.Review;
using LibManage.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class ReviewController : Controller
{
    private readonly IReviewService _reviewService;
    private readonly IBookService _bookService;

    public ReviewController(IReviewService reviewService, IBookService bookService)
    {
        _reviewService = reviewService;
        _bookService = bookService;
    }

    public async Task<IActionResult> Index()
    {
        var reviews = await _reviewService.GetAllAsync();
        return View(reviews);
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var dto = new CreateReviewDto
        {
            BookOptions = (await _bookService.GetAllBooksAsync())
                .Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = $"{b.Title} by {b.AuthorName}"
                })
                .ToList()
        };

        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateReviewDto dto)
    {
        if (!ModelState.IsValid)
        {
            dto.BookOptions = (await _bookService.GetAllBooksAsync())
                .Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = $"{b.Title} by {b.AuthorName}"
                })
                .ToList();

            return View(dto);
        }

        // Get logged-in username (or Id) — example using Identity
        var username = User.Identity?.Name;

        await _reviewService.AddAsync(dto, username!);
        return RedirectToAction(nameof(Index));
    }
}