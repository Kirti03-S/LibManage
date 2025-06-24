namespace LibManage.DTOs.Member
{
    public class MemberDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string Email { get; set; } = "";

        public int MembershipPlanId { get; set; }

        public string MembershipPlanName { get; set; } = "";
        public string? PlanName { get; set; }
    }
}

