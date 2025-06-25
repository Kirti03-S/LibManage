using LibManage.Models.LibManage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class LendingRecordController : Controller
{
    private readonly ILendingService _lendingService;

    public LendingRecordController(ILendingService lendingService)
    {
        _lendingService = lendingService;
    }

    public async Task<IActionResult> MyLendings()
    {
        var records = await _lendingService.GetCurrentLendingsAsync(User.Identity!.Name!);
        return View(records);
    }

    [HttpPost]
    public async Task<IActionResult> Return(int id)
    {
        await _lendingService.ReturnBookAsync(id);
        return RedirectToAction(nameof(MyLendings));
    }
}
