using LibManage.DTOs.Member;
using LibManage.Models;
using LibManage.Repositories;
using LibManage.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepo;
    private readonly IMembershipPlanRepository _planRepo;
    private readonly IBookRepository _bookRepo;

    public MemberService(
        IMemberRepository memberRepo,
        IMembershipPlanRepository planRepo,
        IBookRepository bookRepo)
    {
        _memberRepo = memberRepo;
        _planRepo = planRepo;
        _bookRepo = bookRepo;
    }

    public async Task<List<MemberDto>> GetAllMembersAsync()
    {
        var members = await _memberRepo.GetAllAsync();

        return members.Select(m => new MemberDto
        {
            Id = m.Id,
            Name = m.Name,
            Email = m.Email,
            MembershipPlanId = m.MembershipPlanId,
            PlanName = m.MembershipPlan?.PlanName
        }).ToList();
    }

    public async Task<MemberDto?> GetMemberByIdAsync(int id)
    {
        var m = await _memberRepo.GetByIdAsync(id);
        if (m == null) return null;

        return new MemberDto
        {
            Id = m.Id,
            Name = m.Name,
            Email = m.Email,
            MembershipPlanId = m.MembershipPlanId,
            PlanName = m.MembershipPlan?.PlanName
        };
    }

    public async Task<CreateMemberDto> GetCreateMemberViewModelAsync()
    {
        var plans = await _planRepo.GetAllAsync();
        var books = await _bookRepo.GetAllAsync();

        return new CreateMemberDto
        {
            MembershipPlans = plans.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.PlanName
            }).ToList(),

            AvailableBooks = books.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Title
            }).ToList()
        };
    }

    public async Task<CreateMemberDto> PopulatePlansAsync(CreateMemberDto dto)
    {
        var plans = await _planRepo.GetAllAsync();
        var books = await _bookRepo.GetAllAsync();

        dto.MembershipPlans = plans.Select(p => new SelectListItem
        {
            Value = p.Id.ToString(),
            Text = p.PlanName
        }).ToList();

        dto.AvailableBooks = books.Select(b => new SelectListItem
        {
            Value = b.Id.ToString(),
            Text = b.Title
        }).ToList();

        return dto;
    }

    public async Task CreateMemberAsync(CreateMemberDto dto)
    {
        var selectedBooks = new List<Book>();

        if (dto.SelectedBookIds != null && dto.SelectedBookIds.Any())
        {
            selectedBooks = await _bookRepo.GetBooksByIdsAsync(dto.SelectedBookIds);
        }

        var member = new Member
        {
            Name = dto.Name,
            Email = dto.Email,
            MembershipPlanId = dto.MembershipPlanId,
            SelectedBooks = selectedBooks
        };

        await _memberRepo.AddAsync(member);
    }

    public async Task UpdateMemberAsync(int id, CreateMemberDto dto)
    {
        var member = await _memberRepo.GetByIdAsync(id);
        if (member == null) return;

        // ❌ Do NOT update Name or Email
        // ✅ Only update MembershipPlanId
        member.MembershipPlanId = dto.MembershipPlanId;

        await _memberRepo.UpdateAsync(member);
    }

    public async Task<CreateMemberDto?> GetEditMemberDtoAsync(int id)
    {
        var member = await _memberRepo.GetByIdAsync(id);
        if (member == null) return null;

        var plans = await _planRepo.GetAllAsync();

        return new CreateMemberDto
        {
            Name = member.Name,
            Email = member.Email,
            MembershipPlanId = member.MembershipPlanId,
            MembershipPlans = plans.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.PlanName
            }).ToList()
        };
    }

    public async Task DeleteMemberAsync(int id)
    {
        var member = await _memberRepo.GetByIdAsync(id);
        if (member == null) return;

        await _memberRepo.DeleteAsync(member);
    }
}
