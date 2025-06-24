using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibManage.DTOs.Member;
using Microsoft.AspNetCore.Mvc.Rendering;
using LibManage.Repositories.Interfaces;


[Authorize(Roles = "Admin")]
public class MemberController : Controller
{
    private readonly IMemberService _service;


    public MemberController(IMemberService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var members = await _service.GetAllMembersAsync();
        return View(members);
    }

    public async Task<IActionResult> Details(int id)
    {
        var member = await _service.GetMemberByIdAsync(id);
        if (member == null) return NotFound();
        return View(member);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var viewModel = await _service.GetCreateMemberViewModelAsync(); // ✅ call service
        return View(viewModel);
    }



    [HttpPost]
    public async Task<IActionResult> Create(CreateMemberDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        await _service.CreateMemberAsync(dto);
        return RedirectToAction(nameof(Index));
    }

   

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var dto = await _service.GetEditMemberDtoAsync(id);
        if (dto == null) return NotFound();
        return View(dto);
    }


    [HttpPost]
    public async Task<IActionResult> Edit(int id, CreateMemberDto dto)
    {
        if (!ModelState.IsValid)
        {
            // Repopulate MembershipPlans if the model is invalid
            dto = await _service.PopulatePlansAsync(dto);
            return View(dto);
        }

        await _service.UpdateMemberAsync(id, dto);
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteMemberAsync(id);
        return RedirectToAction(nameof(Index));
    }
}

