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
    var email = User.Identity?.Name!;
    var plans = await _planService.GetAllAsync();
    var existingMember = await _requestService.GetCurrentMemberPlanAsync(email); // ✅ Add this method

    ViewBag.CurrentPlanId = existingMember?.MembershipPlanId;

    return View(plans); // ✅ This is correct
}

    [HttpPost]
    public async Task<IActionResult> Index(CreateMembershipRequestDto dto)
    {
        if (!ModelState.IsValid)
        {
            TempData["Message"] = "Invalid submission.";
            return RedirectToAction("Index");
        }

        var email = User.Identity?.Name!;
        try
        {
            await _requestService.CreateRequestAsync(email, dto.MembershipPlanId);
            TempData["Message"] = "Request submitted. Admin will review it.";
        }
        catch (InvalidOperationException ex)
        {
            TempData["Message"] = ex.Message;
        }

        return RedirectToAction("Index"); // ✅ Always redirect so view uses correct model
    }


}
