using LibManage.DTOs;
using LibManage.DTOs.MembershipPlan;

public interface IMembershipPlanService
{
    Task<List<MembershipPlanDto>> GetAllAsync();
    Task<MembershipPlanDto?> GetByIdAsync(int id);
    Task CreateAsync(CreateMembershipPlanDto dto);
    Task UpdateAsync(CreateMembershipPlanDto dto);
    Task DeleteAsync(int id);
}
