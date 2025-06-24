using LibManage.DTOs.LendingRecord;
using LibManage.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

public class LendingRecordController : Controller
{
    private readonly ILendingRecordService _service;

    public LendingRecordController(ILendingRecordService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var records = await _service.GetAllAsync();
        return View(records);
    }

    [HttpPost]
    public async Task<IActionResult> Return(int id)
    {
        await _service.MarkAsReturnedAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var dto = await _service.GetCreateViewModelAsync();
        return View(dto); // Should match Views/LendingRecord/Create.cshtml
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateLendingRecordDto dto)
    {
        if (!ModelState.IsValid)
        {
            dto = await _service.GetCreateViewModelAsync(); // repopulate dropdowns
            return View(dto);
        }

        await _service.CreateAsync(dto);
        return RedirectToAction(nameof(Index));
    }
}   


