using LibManage.DTOs.MembershipRequest;
using LibManage.Models;

public interface IMembershipRequestService
{
    Task CreateRequestAsync(string email, int planId);
    Task<List<MembershipRequestViewModel>> GetPendingRequestsAsync();
    Task ApproveRequestAsync(int id);
    Task RejectRequestAsync(int requestId);
    Task<MembershipRequestViewModel?> GetRequestByUsernameAsync(string username);
    Task<Member?> GetCurrentMemberPlanAsync(string email);


}

