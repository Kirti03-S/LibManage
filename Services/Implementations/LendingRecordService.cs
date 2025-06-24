using LibManage.Data;
using LibManage.Repositories.Interfaces;
using LibManage.Repositories;
using Microsoft.EntityFrameworkCore;

public class LendingService : ILendingService
{
    private readonly LibraryDbContext _context;
    private readonly IMemberRepository _memberRepo;
    private readonly IBookRepository _bookRepo;

    public LendingService(LibraryDbContext context, IMemberRepository memberRepo, IBookRepository bookRepo)
    {
        _context = context;
        _memberRepo = memberRepo;
        _bookRepo = bookRepo;
    }

    public async Task<string> BorrowBookAsync(string username, int bookId)
    {
        var member = await _memberRepo.GetByEmailAsync(username);
        if (member == null)
            return "You must be a registered member to borrow books.";

        var plan = await _context.MembershipPlans.FindAsync(member.MembershipPlanId);
        if (plan == null)
            return "Invalid membership plan.";

        var currentBorrowed = await _context.LendingRecords
            .Where(l => l.MemberId == member.Id && l.ReturnedDate == null)
            .CountAsync();

        if (currentBorrowed >= plan.MaxBooksAllowed)
            return $"You have already borrowed the maximum number of books ({plan.MaxBooksAllowed}).";

        var book = await _bookRepo.GetByIdAsync(bookId);
        if (book == null || book.Stock <= 0)
            return "Book is not available for lending.";

        var lending = new LendingRecord
        {
            BookId = bookId,
            MemberId = member.Id,
            BorrowedDate = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(plan.DurationInDays)
        };

        _context.LendingRecords.Add(lending);
        book.Stock -= 1;

        await _context.SaveChangesAsync();
        return "Book successfully borrowed.";
    }

    public async Task<List<LendingRecord>> GetCurrentLendingsAsync(string username)
    {
        var member = await _memberRepo.GetByEmailAsync(username);
        if (member == null) return new List<LendingRecord>();

        return await _context.LendingRecords
            .Include(l => l.Book)
            .Where(l => l.MemberId == member.Id && l.ReturnedDate == null)
            .ToListAsync();
    }
}
