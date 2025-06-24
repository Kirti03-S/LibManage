using LibManage.Data;
using LibManage.Models;
using LibManage.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibManage.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly LibraryDbContext _context;

        public MemberRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Member>> GetAllAsync()
        {
            return await _context.Members.Include(m => m.MembershipPlan).ToListAsync();
        }

        public async Task<Member?> GetByIdAsync(int id)
        {
            return await _context.Members.Include(m => m.MembershipPlan)
                                         .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Member member)
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Member member)
        {
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
        }

        public async Task<Member?> GetByEmailAsync(string email)
        {
            return await _context.Members
                .Include(m => m.SelectedBooks)
                .FirstOrDefaultAsync(m => m.Email == email);
        }

    }
}
