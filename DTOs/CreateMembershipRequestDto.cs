using Microsoft.AspNetCore.Mvc.Rendering;

public class CreateMembershipRequestDto
{
    public int MembershipPlanId { get; set; }

    public List<SelectListItem>? AvailablePlans { get; set; }
    public List<SelectListItem>? AvailableBooks { get; set; }

    public List<int> SelectedBookIds { get; set; } = new();
}

