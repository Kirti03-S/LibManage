using LibManage.DTOs.MembershipRequest;
using LibManage.Models;

public interface IMembershipRequestService
{
    Task CreateRequestAsync(string email, int planId, List<Book> books);
    Task<List<MembershipRequestViewModel>> GetPendingRequestsAsync();
    Task ApproveRequestAsync(int id);
    Task RejectRequestAsync(int requestId);
    Task<MembershipRequestViewModel?> GetRequestByUsernameAsync(string username);


}

