using LibManage.Models;

public class MembershipRequest
{
    public int Id { get; set; }

    public string UserEmail { get; set; } = "";

    public int MembershipPlanId { get; set; }
    public MembershipPlan? MembershipPlan { get; set; }

    public List<Book> SelectedBooks { get; set; } = new();

    public DateTime RequestedOn { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Pending";
}

