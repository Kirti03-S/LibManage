using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibManage.DTOs.MembershipPlan
{
    public class CreateMembershipRequestDto
    {
        public int MembershipPlanId { get; set; }

        public List<SelectListItem> AvailablePlans { get; set; } = new();
    }
}
