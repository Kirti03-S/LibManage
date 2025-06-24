using LibManage.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
public class MembershipRequestAdminController : Controller
{
    private readonly IMembershipRequestService _requestService;

    public MembershipRequestAdminController(IMembershipRequestService requestService)
    {
        _requestService = requestService;
    }

    public async Task<IActionResult> Index()
    {
        var requests = await _requestService.GetPendingRequestsAsync();
        return View(requests);
    }

    [HttpPost]
    public async Task<IActionResult> Approve(int id)
    {
        await _requestService.ApproveRequestAsync(id);
        TempData["Success"] = "Membership request approved.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Reject(int id)
    {
        await _requestService.RejectRequestAsync(id);
        TempData["Error"] = "Membership request rejected.";
        return RedirectToAction(nameof(Index));
    }
}

