using LibManage.DTOs.MembershipPlan;
using LibManage.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

[Authorize]
public class JoinMembershipController : Controller
{
    private readonly IMembershipPlanService _planService;
    private readonly IMembershipRequestService _requestService;

    public JoinMembershipController(
        IMembershipPlanService planService,
        IMembershipRequestService requestService)
    {
        _planService = planService;
        _requestService = requestService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var plans = await _planService.GetAllAsync();
        return View(plans); // View expects List<MembershipPlanDto>
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

            return View(dto);
        }

        var email = User.Identity?.Name!;
        await _requestService.CreateRequestAsync(email, dto.MembershipPlanId);

        TempData["Message"] = "Request submitted. Admin will review it.";
        return RedirectToAction("Index", "Home");
    }
}
