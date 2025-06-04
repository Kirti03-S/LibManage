
namespace LibManage.Models;

public class Member
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";

    public int MembershipPlanId { get; set; }
    public MembershipPlan? MembershipPlan { get; set; }

    public ICollection<LendingRecord>? LendingRecords { get; set; }
    public ICollection<Review>? Reviews { get; set; }
}

