using LibManage.Data;
using LibManage.DTOs.MembershipRequest;
using LibManage.Models;
using LibManage.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class MembershipRequestService : IMembershipRequestService
{
    private readonly LibraryDbContext _context;
    private readonly IMemberRepository _memberRepo;

    public MembershipRequestService(LibraryDbContext context, IMemberRepository memberRepo)
    {
        _context = context;
        _memberRepo = memberRepo;
    }

    public async Task CreateRequestAsync(string email, int planId)
    {
        // Get the latest membership request
        var existingRequest = await _context.MembershipRequests
            .Where(r => r.UserEmail == email && (r.Status == "Pending" || r.Status == "Approved"))
            .OrderByDescending(r => r.RequestedOn)
            .FirstOrDefaultAsync();

        if (existingRequest != null)
        {
            // Allow only if new plan is an upgrade
            if (planId <= existingRequest.MembershipPlanId)
            {
                throw new InvalidOperationException("You already have a membership request. You can only request an upgrade to a higher plan.");
            }
        }

        var request = new MembershipRequest
        {
            UserEmail = email,
            MembershipPlanId = planId,
            Status = "Pending",
            RequestedOn = DateTime.UtcNow
        };

        _context.MembershipRequests.Add(request);
        await _context.SaveChangesAsync();
    }

    public async Task<List<MembershipRequestViewModel>> GetPendingRequestsAsync()
    {
        var requests = await _context.MembershipRequests
            .Include(r => r.MembershipPlan)
            .Include(r => r.SelectedBooks)
            .Where(r => r.Status == "Pending")
            .ToListAsync();

        return requests.Select(r => new MembershipRequestViewModel
        {
            Id = r.Id,
            UserEmail = r.UserEmail,
            PlanName = r.MembershipPlan?.PlanName ?? "",
            BookTitles = r.SelectedBooks.Select(b => b.Title).ToList(),
            Status = r.Status,
            RequestedOn = r.RequestedOn
        }).ToList();
    }


    public async Task ApproveRequestAsync(int id)
    {
        var request = await _context.MembershipRequests
            .Include(r => r.SelectedBooks)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (request == null || request.Status == "Approved") return;

        var member = await _memberRepo.GetByEmailAsync(request.UserEmail);
        if (member == null)
        {
            member = new Member
            {
                Email = request.UserEmail,
                Name = request.UserEmail,
                MembershipPlanId = request.MembershipPlanId,
                SelectedBooks = request.SelectedBooks
            };
            await _memberRepo.AddAsync(member);
        }
        else
        {
            member.MembershipPlanId = request.MembershipPlanId;
            member.SelectedBooks = request.SelectedBooks;
            await _memberRepo.UpdateAsync(member);
        }

        request.Status = "Approved";
        await _context.SaveChangesAsync();
    }
    public async Task RejectRequestAsync(int requestId)
    {
        var request = await _context.MembershipRequests.FindAsync(requestId);
        if (request == null) return;

        request.Status = "Rejected";
        await _context.SaveChangesAsync();
    }
    public async Task<MembershipRequestViewModel?> GetRequestByUsernameAsync(string username)
    {
        var request = await _context.MembershipRequests
            .Include(r => r.MembershipPlan)
            .Where(r => r.UserEmail == username)
            .OrderByDescending(r => r.RequestedOn)
            .FirstOrDefaultAsync();

        if (request == null) return null;

        return new MembershipRequestViewModel
        {
            UserEmail = request.UserEmail,
            Status = request.Status,
            PlanName = request.MembershipPlan?.PlanName ?? "N/A",
            BookTitles = request.SelectedBooks.Select(b => b.Title).ToList(),
            RequestedOn = request.RequestedOn
        };
    }

    public Task CreateRequestAsync(string email, int planId, List<Book> books)
    {
        throw new NotImplementedException();
    }
    public async Task<Member?> GetCurrentMemberPlanAsync(string email)
    {
        return await _context.Members.FirstOrDefaultAsync(m => m.Email == email);
    }
}

