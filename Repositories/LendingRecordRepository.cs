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

        public async Task<List<LendingRecord>> GetAllAsync()
        {
            return await _context.LendingRecords
                .Include(l => l.Book)
                .Include(l => l.Member)
                .ToListAsync();
        }

        public async Task<LendingRecord?> GetByIdAsync(int id)
        {
            return await _context.LendingRecords
                .Include(l => l.Book)
                .Include(l => l.Member)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task AddAsync(LendingRecord record)
        {
            await _context.LendingRecords.AddAsync(record);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LendingRecord record)
        {
            _context.LendingRecords.Update(record);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var record = await _context.LendingRecords.FindAsync(id);
            if (record != null)
            {
                _context.LendingRecords.Remove(record);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(LendingRecord record)
        {
            _context.LendingRecords.Remove(record);
            await _context.SaveChangesAsync();
        }

    }
}

