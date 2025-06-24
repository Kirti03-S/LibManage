using LibManage.Models.LibManage.Models;
using LibManage.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class JoinMembershipController : Controller
{
    private readonly IMembershipPlanService _planService;
    private readonly IMembershipRequestService _requestService;
    private readonly IBookRepository _bookRepo;

    public JoinMembershipController(
        IMembershipPlanService planService,
        IMembershipRequestService requestService,
        IBookRepository bookRepo)
    {
        _planService = planService;
        _requestService = requestService;
        _bookRepo = bookRepo;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var dto = new CreateMembershipRequestDto
        {
            AvailablePlans = (await _planService.GetAllAsync())
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.PlanName
                }).ToList(),

            AvailableBooks = (await _bookRepo.GetAllAsync())
                .Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Title
                }).ToList()
        };

        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Index(CreateMembershipRequestDto dto)
    {
        if (!ModelState.IsValid)
        {
            dto.AvailablePlans = (await _planService.GetAllAsync())
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.PlanName
                }).ToList();

            dto.AvailableBooks = (await _bookRepo.GetAllAsync())
                .Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Title
                }).ToList();

            return View(dto);
        }

        var email = User.Identity?.Name!;
        var selectedBooks = await _bookRepo.GetBooksByIdsAsync(dto.SelectedBookIds);
        var plan = await _planService.GetByIdAsync(dto.MembershipPlanId);

        if (selectedBooks.Count > plan.MaxBooksAllowed)
        {
            ModelState.AddModelError("", $"You can only select up to {plan.MaxBooksAllowed} books.");
            return View(dto);
        }

        await _requestService.CreateRequestAsync(email, dto.MembershipPlanId, selectedBooks);

        TempData["Message"] = "Request submitted. Admin will review it.";
        return RedirectToAction("Index", "Home");
    }
}
