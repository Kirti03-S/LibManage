namespace LibManage.DTOs.LendingRecord;

public class LendingRecordDto
{
    public string BookTitle { get; set; } = "";
    public DateTime BorrowedDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnedDate { get; set; }
}
