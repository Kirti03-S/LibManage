using LibManage.DTOs;
using LibManage.DTOs.MembershipPlan;
using LibManage.Models;
using LibManage.Repositories;
using LibManage.Services.Interfaces;

public class MembershipPlanService : IMembershipPlanService
{
    private readonly IMembershipPlanRepository _repository;

    public MembershipPlanService(IMembershipPlanRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<MembershipPlanDto>> GetAllAsync()
    {
        var plans = await _repository.GetAllAsync();
        return plans.Select(p => new MembershipPlanDto
        {
            Id = p.Id,
            PlanName = p.PlanName,
            MaxBooksAllowed = p.MaxBooksAllowed,
            DurationInDays = p.DurationInDays
        }).ToList();
    }

    public async Task<MembershipPlanDto?> GetByIdAsync(int id)
    {
        var plan = await _repository.GetByIdAsync(id);
        if (plan == null) return null;

        return new MembershipPlanDto
        {
            Id = plan.Id,
            PlanName = plan.PlanName,
            MaxBooksAllowed = plan.MaxBooksAllowed,
            DurationInDays = plan.DurationInDays
        };
    }

    public async Task CreateAsync(CreateMembershipPlanDto dto)
    {
        var plan = new MembershipPlan
        {
            PlanName = dto.PlanName,
            MaxBooksAllowed = dto.MaxBooksAllowed,
            DurationInDays = dto.DurationInDays
        };

        await _repository.AddAsync(plan);
    }

    public async Task UpdateAsync(CreateMembershipPlanDto dto)
    {
        var plan = await _repository.GetByIdAsync(dto.Id);
        if (plan == null) return;

        plan.PlanName = dto.PlanName;
        plan.MaxBooksAllowed = dto.MaxBooksAllowed;
        plan.DurationInDays = dto.DurationInDays;

        await _repository.UpdateAsync(plan);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
