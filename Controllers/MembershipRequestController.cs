using LibManage.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LibManage.DTOs.MembershipRequest;

[Authorize]
public class MembershipRequestController : Controller
{
    private readonly IMembershipRequestService _requestService;

    public MembershipRequestController(IMembershipRequestService requestService)
    {
        _requestService = requestService;
    }

    public async Task<IActionResult> Status()
    {
        var username = User.Identity?.Name;

        if (string.IsNullOrEmpty(username))
        {
            TempData["Error"] = "User identity not found.";
            return RedirectToAction("Index", "Home");
        }

        var request = await _requestService.GetRequestByUsernameAsync(username); // renamed method

        if (request == null)
        {
            return RedirectToAction("Create", "MembershipRequest");
        }

        ViewBag.Message = request.Status switch
        {
            "Pending" => "⏳ Your membership request is pending approval.",
            "Approved" => "✅ Your membership has been approved! You can now borrow books.",
            "Rejected" => "❌ Your membership request was rejected. Please contact the admin.",
            _ => "ℹ️ Unknown request status."
        };

        return View("Status", request);
    }
}
