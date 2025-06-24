using LibManage.Data;
using LibManage.Models;
using LibManage.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibManage.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly LibraryDbContext _context;

        public ReviewRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await _context.Reviews
                .Include(r => r.Book)
                .Include(r => r.Member)
                .ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _context.Reviews
                .Include(r => r.Book)
                .Include(r => r.Member)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Review review)
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Review review)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }
}

