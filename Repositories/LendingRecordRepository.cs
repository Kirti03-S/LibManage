using LibManage.Data;
using LibManage.Models;
using LibManage.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibManage.Repositories
{

    public class LendingRecordRepository : ILendingRecordRepository
    {
        private readonly LibraryDbContext _context;

        public LendingRecordRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<LendingRecord>> GetRecordsByMemberIdAsync(int memberId)
        {
            return await _context.LendingRecords
                .Include(r => r.Book)
                .Where(r => r.MemberId == memberId)
                .ToListAsync();
        }
    }

}

