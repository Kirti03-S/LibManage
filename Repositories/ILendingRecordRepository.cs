using LibManage.Models;

public interface ILendingRecordRepository
{
    Task<List<LendingRecord>> GetRecordsByMemberIdAsync(int memberId);


}

