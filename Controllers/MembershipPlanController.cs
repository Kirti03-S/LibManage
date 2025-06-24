using LibManage.Services.Interfaces;
using LibManage.DTOs.MembershipPlan;
using Microsoft.AspNetCore.Mvc;
using LibManage.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace LibManage.Controllers
{
    [Authorize(Roles ="Admin")]
    public class MembershipPlanController : Controller
    {
        private readonly IMembershipPlanService _membershipPlanService;

        public MembershipPlanController(IMembershipPlanService membershipPlanService)
        {
            _membershipPlanService = membershipPlanService;
        }

        public async Task<IActionResult> Index()
        {
            var plans = await _membershipPlanService.GetAllAsync();
            return View(plans);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var plan = await _membershipPlanService.GetByIdAsync(id);
            if (plan == null) return NotFound();
            return View(plan);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CreateMembershipPlanDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _membershipPlanService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMembershipPlanDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _membershipPlanService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _membershipPlanService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
