using LibManage.DTOs.Review;
using LibManage.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

public class ReviewController : Controller
{
    private readonly IReviewService _service;

    public ReviewController(IReviewService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var reviews = await _service.GetAllAsync();
        return View(reviews);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(ReviewDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        await _service.AddAsync(dto);
        return RedirectToAction(nameof(Index));
    }
}

