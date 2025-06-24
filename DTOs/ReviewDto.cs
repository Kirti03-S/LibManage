namespace LibManage.DTOs.Review
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; } = "";
        public int MemberId { get; set; }
        public string MemberName { get; set; } = "";
        public int Rating { get; set; }
        public string Comment { get; set; } = "";
        public DateTime CreatedDate { get; set; }
    }
}

