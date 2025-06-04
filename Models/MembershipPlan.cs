namespace LibManage.Models;

public class MembershipPlan
{
    public int Id { get; set; }
    public string PlanName { get; set; } = "";
    public int MaxBooksAllowed { get; set; }
    public int DurationInDays { get; set; }

    public ICollection<Member>? Members { get; set; }
}

