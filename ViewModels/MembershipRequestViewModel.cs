namespace LibManage.DTOs.MembershipRequest
{
    public class MembershipRequestViewModel
    {
        public int Id { get; set; }
        public string UserEmail { get; set; } = "";
        public string PlanName { get; set; } = "";
        public List<string> BookTitles { get; set; } = new();
        public string Status { get; set; } = "";
        public DateTime RequestedOn { get; set; }
    }
}

