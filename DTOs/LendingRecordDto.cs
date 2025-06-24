namespace LibManage.DTOs.LendingRecord
{
    public class LendingRecordDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }      
        public int MemberId { get; set; }     
        public string BookTitle { get; set; } = "";
        public string MemberName { get; set; } = "";
        public DateTime BorrowedDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
    }
}
