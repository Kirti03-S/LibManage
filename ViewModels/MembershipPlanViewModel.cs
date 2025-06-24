namespace LibManage.ViewModels
{
    public class MembershipPlanViewModel
    {
        public int Id { get; set; }
        public string PlanName { get; set; } = "";
        public int MaxBooksAllowed { get; set; }
        public int DurationInDays { get; set; }
    }
}
