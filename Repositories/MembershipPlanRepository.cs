using LibManage.Data; // this is your DbContext namespace
using LibManage.Models;
using Microsoft.EntityFrameworkCore;

namespace LibManage.Repositories
{
    public class MembershipPlanRepository : IMembershipPlanRepository
    {
        private readonly LibraryDbContext _context;

        public MembershipPlanRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<MembershipPlan>> GetAllAsync()
        {
            return await _context.MembershipPlans.ToListAsync();
        }

        public async Task<MembershipPlan?> GetByIdAsync(int id)
        {
            return await _context.MembershipPlans.FindAsync(id);
        }

        public async Task AddAsync(MembershipPlan plan)
        {
            _context.MembershipPlans.Add(plan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MembershipPlan plan)
        {
            _context.MembershipPlans.Update(plan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var plan = await _context.MembershipPlans.FindAsync(id);
            if (plan != null)
            {
                _context.MembershipPlans.Remove(plan);
                await _context.SaveChangesAsync();
            }
        }
    }
}
