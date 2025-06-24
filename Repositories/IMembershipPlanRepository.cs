using LibManage.Models;

public interface IMembershipPlanRepository
{
    Task<List<MembershipPlan>> GetAllAsync();
    Task<MembershipPlan?> GetByIdAsync(int id);
    Task AddAsync(MembershipPlan plan);
    Task UpdateAsync(MembershipPlan plan);
    Task DeleteAsync(int id);
}
