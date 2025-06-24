namespace LibManage.DTOs
{
    public class CreateMembershipPlanDto
    {
        public int Id { get; set; } 
        public string PlanName { get; set; } = string.Empty;
        public int MaxBooksAllowed { get; set; }
        public int DurationInDays { get; set; }
    }

}
