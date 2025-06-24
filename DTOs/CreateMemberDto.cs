using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibManage.DTOs.Member
{
    public class CreateMemberDto
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Membership Plan")]
        public int MembershipPlanId { get; set; }

        // Used to show dropdown in view
        public List<SelectListItem>? MembershipPlans { get; set; } = new();
        public List<SelectListItem>? AvailableBooks { get; set; } = new();

        [Display(Name = "Select Books")]
        public List<int>? SelectedBookIds { get; set; } = new();
        public int MaxBooksAllowed { get; set; }

    }
}
