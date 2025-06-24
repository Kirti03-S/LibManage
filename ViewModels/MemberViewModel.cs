namespace LibManage.ViewModels
{
    public class MemberViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string PlanName { get; set; } = "";
        public int MembershipPlanId { get; set; }
    }
}