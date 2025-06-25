using LibManage.DTOs.Review;
using LibManage.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public async Task<IActionResult> Create()
        {
            var dto = new CreateReviewDto
            {
                BookOptions = (await _service.GetAllAsync())
                    .Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.BookTitle })
                    .ToList()
            };

            return View(dto);
        }

    [HttpPost]
    public async Task<IActionResult> Create(CreateReviewDto dto)
    {
        if (!ModelState.IsValid)
        {
            dto.BookOptions = (await _service.GetAllAsync())
                .Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.BookTitle })
                .ToList();

            return View(dto);
        }

        // Get logged-in username (or Id) — example using Identity
        var username = User.Identity?.Name;

        await _service.AddAsync(dto, username!);
        return RedirectToAction(nameof(Index));
    }


}

