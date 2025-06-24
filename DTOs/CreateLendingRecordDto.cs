using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LibManage.DTOs.LendingRecord
{
    public class CreateLendingRecordDto
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public int MemberId { get; set; }

        [Required]
        public DateTime BorrowedDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        // For dropdowns
        public List<SelectListItem> Books { get; set; } = new();
        public List<SelectListItem> Members { get; set; } = new();
    }
}
