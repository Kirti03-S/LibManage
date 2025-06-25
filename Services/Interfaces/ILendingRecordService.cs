using LibManage.DTOs.LendingRecord;

public interface ILendingService
{
    Task<string> BorrowBookAsync(string username, int bookId);
    Task<List<LendingRecord>> GetCurrentLendingsAsync(string username);
    Task ReturnBookAsync(int lendingRecordId);

}
